using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;

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
                .AddImpoert(string.Format(".json.response.*", package))
                .AddImpoert(string.Format(".exception.*", package,model_class))
                .AddImpoert(string.Format("{0}.mapper.{1}", package, mapper_class));
            //添加注解
            javaClass.AddAnnotation("@Service")
                .AddValue("@Autowired", mapper_class, mapper_value);


            //Insert函数
            JavaMethod insert = javaClass.AddInterfaceMethord("Insert", model_class)
                .addParam(model_class, model_value);
            foreach (DbColumn col in table.Column){
                if (!col.IsKey)
                    insert.addParam(col.getJavaTyep(), col.Name);
            }
            insert.addLogicNo(string.Format("{0} {1} = new {0}", model_class, model_value));

                //update函数
            javaClass.AddInterfaceMethord("update", "model_class")
                .addThrow("ErrorCodeException")
                .addParam(model_class, model_value);

            //selectByPrimaryKey函数
            javaClass.AddInterfaceMethord("selectByPrimaryKey", model_class)
                .addThrow("ErrorCodeException")
                .addParam(table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //selectPage函数
            javaClass.AddInterfaceMethord("selectPage", string.Format("PageResponse<{0}>", model_class))
                .addParam("Integer", "offset")
                .addParam("Integer", "pageSize");

            //deletePrimaryKey函数
            javaClass.AddInterfaceMethord("deleteByPrimaryKey", "int")
                .addParam( table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //输出Mapper
            WriteFile(path + "\\service", toFirstUp(table.getClassName() + "Service") + ".java", javaClass.toListString());

        }
    }
}
 