using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;

namespace SpringBoot.Auto
{
    public class AutoFegin : AutoClass
    {

        public static void CreateFeign(DbTable table, string package, string path)
        {
            string model_class = toFirstUp(table.getClassName());
            string model_value = toFirstLow(table.getClassName());

            //创建Feign类
            JavaClass javaClass = new JavaClass("I" + toFirstUp(table.getClassName() + "Feign"), package, true);
            //头文件
            javaClass.AddImpoert("org.springframework.web.bind.annotation.*")
                .AddImpoert("java.util.Date")
                .AddImpoert("org.springframework.cloud.netflix.feign.FeignClient")
                .AddImpoert(string.Format("{0}.json.JsonBean", package))
                .AddImpoert(string.Format("{0}.json.response.PageResponse", package))
                .AddImpoert(string.Format("{0}.model.{1}", package, model_class));
            //添加注解
            javaClass
                .AddAnnotation(string.Format("@RequestMapping(value = \"/v1/{0}\")", table.getClassLowName()))
                .AddAnnotation("@FeignClient(\"\")");

            //Insert函数
            //--------------------
            JavaMethod insert = javaClass.AddInterfaceMethord("insert", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation("@RequestMapping(value = \"\",method = RequestMethod.POST)");
            string param = "";
            int index = 0;
            foreach (DbColumn col in table.Column)
            {
                if (!col.IsKey)
                {
                    insert.addParam(string.Format("@RequestParam(value = \"{2}\")", col.Notes, col.IsNotNull.ToString().ToLower(), col.Name), col.getJavaTyep(), col.Name);
                    if (index == 0)
                        param += col.Name;
                    else
                        param += "," + col.Name;
                    index++;
                }
            }


            //update函数
            //------------------
            JavaMethod update = javaClass.AddInterfaceMethord("update", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.PUT)", table.getColumnKey().Name));
            param = "";
            index = 0;
            foreach (DbColumn col in table.Column)
            {
                if (col.IsKey)
                    update.addParam("@PathVariable(value = \"id\")", col.getJavaTyep(),col.Name);
                else
                    update.addParam(string.Format("@RequestParam(value = \"{2}\")", col.Notes, "false", col.Name), col.getJavaTyep(), col.Name);
                if (index == 0) 
                    param += col.Name;
                else
                    param += "," + col.Name;
                index++;
            }

            //selectByPrimaryKey函数
            //------------------
            JavaMethod selectByPrimaryKey = javaClass.AddInterfaceMethord("selectByPrimaryKey", string.Format("JsonBean<{0}>", model_class))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.GET)", table.getColumnKey().Name));

            selectByPrimaryKey.addParam("@PathVariable(value = \"id\")", table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //selectPage函数
            //------------------
            JavaMethod selectPage = javaClass.AddInterfaceMethord("selectPage", string.Format("JsonBean<PageResponse<{0}>>", model_class))
                .addAnnotation(string.Format("@RequestMapping(value = \"/page\",method = RequestMethod.GET)"))
                .addParam("@RequestParam(value = \"offset\")", "Integer", "offset")
                .addParam("@RequestParam(value = \"pageSize\")", "Integer", "pageSize");

            //deleteByPrimaryKey函数
            //------------------
            JavaMethod deleteByPrimaryKey = javaClass.AddInterfaceMethord("deletePrimaryKey", string.Format("JsonBean"))
                .addAnnotation(string.Format("@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.DELETE)", table.getColumnKey().Name));

            deleteByPrimaryKey.addParam("@PathVariable(value = \"id\")", table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //输出Controller
            WriteFile(path + "\\fegin", toFirstUp("I" + table.getClassName() + "Feign") + ".java", javaClass.toListString());

        }
    }
}
 