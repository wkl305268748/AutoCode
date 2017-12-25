using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;
using System.IO;

namespace SpringBoot.Auto
{
    public class AutoService : AutoClass
    {

        public static void CreateService(DbTable table, string package, string path)
        {
            string model_class = toFirstUp(table.getClassName());
            string model_value = toFirstLow(table.getClassName());
            string mapper_class = toFirstUp(table.getClassName() + "Mapper");
            string mapper_value = toFirstLow(table.getClassName() + "Mapper");

            //创建Service类
            JavaClass javaClass = new JavaClass(toFirstUp(table.getClassName() + "Service"), package);
            //头文件
            javaClass.AddImpoert("org.springframework.beans.factory.annotation.Autowired")
                .AddImpoert("org.springframework.stereotype.Service")
                .AddImpoert("java.util.List")
                .AddImpoert("java.util.Date")
                .AddImpoert("java.util.ArrayList")
                .AddImpoert(string.Format("{0}.json.response.PageResponse", package))
                .AddImpoert(string.Format("{0}.exception.ErrorCodeException", package))
                .AddImpoert(string.Format("{0}.model.{1}", package,model_class))
                .AddImpoert(string.Format("{0}.mapper.{1}", package, mapper_class));
            //添加注解
            javaClass.AddAnnotation("@Service")
                .AddValue("@Autowired", mapper_class, mapper_value);


            //Insert函数
            //--------------------
            JavaMethod insert = javaClass.AddMethord("insert", model_class);
            foreach (DbColumn col in table.Column){
                if (col.Name.Equals("time"))
                    continue;
                if (!col.IsKey)
                    insert.addParam(col.getJavaTyep(), col.Name);
            }
            insert.addLogicNo(string.Format("{0} {1} = new {0}();", model_class, model_value));
            //set语句
            foreach (DbColumn col in table.Column)
            {
                if (!col.IsKey) {
                    if (col.Name.Equals("time")) { 
                        insert.addLogicNo(string.Format("{0}.set{1}({2});", model_value, toFirstUp(col.Name), "new Date()"));
                        continue;
                    }
                    insert.addLogicNo(string.Format("{0}.set{1}({2});", model_value, toFirstUp(col.Name), col.Name));
                }
            }
            insert.addLogicNo(string.Format("{0}.insert({1});",mapper_value,model_value));
            insert.addLogicNo(string.Format("return {0};", model_value));

            //update函数
            //------------------
            JavaMethod update = javaClass.AddMethord("update", model_class)
                .addThrow("ErrorCodeException");
            foreach (DbColumn col in table.Column)
            {
                if (col.Name.Equals("time"))
                    continue;
                update.addParam(col.getJavaTyep(), col.Name);
            }
            update.addLogicNo(string.Format("{0} {1} = {2}.selectByPrimaryKey({3});",model_class,model_value,mapper_value,table.getColumnKey().Name));
            update.addLogicIf(string.Format("{0} == null", model_value))
                .addIfCode("throw new ErrorCodeException(ErrorCodeException.DATA_NO_ERROR);");
            //set语句
            foreach (DbColumn col in table.Column)
            {
                if (!col.IsKey)
                {
                    if (col.Name.Equals("time"))
                        continue;

                    update.addLogicIf(string.Format("{0} != null", col.Name))
                        .addIfCode(string.Format("{0}.set{1}({2});", model_value, toFirstUp(col.Name), col.Name));
                }
            }
            update.addLogicNo(string.Format("{0}.update({1});", mapper_value, model_value));
            update.addLogicNo(string.Format("return {0};", model_value));

            //selectByPrimaryKey函数
            //------------------
            JavaMethod selectByPrimaryKey = javaClass.AddMethord("selectByPrimaryKey", model_class)
                .addThrow("ErrorCodeException")
                .addParam(table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);
            selectByPrimaryKey.addLogicNo(string.Format("{0} {1} = {2}.selectByPrimaryKey({3});", model_class, model_value, mapper_value, table.getColumnKey().Name));
            selectByPrimaryKey.addLogicIf(string.Format("{0} == null", model_value))
                .addIfCode("throw new ErrorCodeException(ErrorCodeException.DATA_NO_ERROR);");
            selectByPrimaryKey.addLogicNo(string.Format("return {0};", model_value));

            //selectPage函数
            //------------------
            JavaMethod selectPage = javaClass.AddMethord("selectPage", string.Format("PageResponse<{0}>", model_class))
                .addParam("Integer", "offset")
                .addParam("Integer", "pageSize");
            selectPage.addLogicNo(string.Format("PageResponse<{0}> response = new PageResponse();",model_class));
            selectPage.addLogicNo(string.Format("response.setItem({0}.selectPage(offset,pageSize));", mapper_value));
            selectPage.addLogicNo(string.Format("response.setTotal({0}.count());", mapper_value));
            selectPage.addLogicNo("response.setOffset(offset);");
            selectPage.addLogicNo("response.setPageSize(pageSize);");
            selectPage.addLogicNo("return response;");

            //deletePrimaryKey函数
            //------------------
            JavaMethod deletePrimaryKey = javaClass.AddMethord("deleteByPrimaryKey", "void")
                .addThrow("ErrorCodeException")
                .addParam(table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);
            deletePrimaryKey.addLogicIf(string.Format("{0}.deleteByPrimaryKey({1}) != 1", mapper_value, table.getColumnKey().Name))
                        .addIfCode("throw new ErrorCodeException(ErrorCodeException.DB_ERROR);");

            //输出Service
            WriteFile(path + "\\service", toFirstUp(table.getClassName() + "Service") + ".java", javaClass.toListString());

            //输出其他辅助文件
            CreateFile("JsonBean", path + "\\json", package, Properties.Resources.JsonBean);
            CreateFile("PageResponse", path + "\\json\\response", package, Properties.Resources.PageResponse);
            CreateFile("ErrorCode", path + "\\exception", package, Properties.Resources.ErrorCode);
            CreateFile("ErrorCodeException", path + "\\exception", package, Properties.Resources.ErrorCodeException);
        }

        private static void CreateFile(string name, string pathDir, string package,string text)
        {
            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(false);  // 用true来指定包含bom
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = string.Format("{0}\\{1}.java", pathDir, name);
            string Text = text.Replace("#package#", package).Replace("#notes#", "Created by AutoCode on " + DateTime.Now.ToShortDateString());
            StreamWriter Writer = new StreamWriter(filePath, false, utf8WithBom);
            Writer.Write(Text);
            Writer.Close();
        }
    }
}
 