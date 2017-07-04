using SpringBoot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpringBoot.Code
{
    //生成Controller类
    public class ControllerHelp : ClassHelp
    {
        public static void CreateController(string path, string pageName, string tableName, List<Field> field)
        {
            //创建文件夹
            string pathDir = path + "\\" + "controller";
            if (!Directory.Exists(pathDir)){
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\" + getControllerName(tableName) + ".java";

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);

            //写入文件
            //包名
            writer.WriteLine(getPackAge(pageName + ".controller"));
            writer.WriteLine();
            //引入头文件
            writer.WriteLine(getImport("org.apache.ibatis.annotations.Param"));
            writer.WriteLine(getImport("org.springframework.beans.factory.annotation.Autowired"));
            writer.WriteLine(getImport("org.springframework.web.bind.annotation.*"));
            writer.WriteLine(getImport("io.swagger.annotations.*"));
            writer.WriteLine(getImport(pageName + ".model.*"));
            writer.WriteLine(getImport(pageName + ".mapper.*"));
            writer.WriteLine(getImport(pageName + ".service.*"));
            writer.WriteLine(getImport(pageName + ".json.response.*"));
            writer.WriteLine(getImport(pageName + ".json.*"));
            writer.WriteLine(getImport(pageName + ".exception.*"));
            writer.WriteLine(getImport("java.util.List"));
            writer.WriteLine(getImport("java.util.Date"));
            writer.WriteLine(getImport("java.util.ArrayList"));
            writer.WriteLine();
            //引入注释
            writer.WriteLine(getNotes("Created by AutoCode on " + DateTime.Now.ToShortDateString()));
            //引入类
            writer.WriteLine("@Api(value = \"/v1/{0}\", description = \"{0}模块\")",getUrlName(tableName));
            writer.WriteLine("@RequestMapping(value = \"/v1/{0}\")", getUrlName(tableName));
            writer.WriteLine("@RestController");
            writer.WriteLine(getClass(getUrlName(tableName), GetClassInfo(tableName, getMapperName(tableName),getClassName(tableName),field)));
            writer.Close();
        }

        private static string GetClassInfo(string table,string serviceClass, string modelClass, List<Field> fields) {
            string result = "\r\n";
            result += GetVariable(serviceClass) + "\r\n\r\n";
            result += GetPageMethod(modelClass, serviceClass);

            return result;
        }

        private static string GetVariable(string serviceClass)
        {
            string result = "";
            result += "\t@Autowired\r\n";
            result += string.Format("\tprivate {0} {1};\r\n", serviceClass, toFirstLow(serviceClass));
            return result;
        }


        private static string GetPageMethod(string modelClass, string serviceClass)
        {

            string function = "";
            function += string.Format("\t@ApiOperation(value = \"列出所有的\")\r\n", modelClass);
            function += string.Format("\t@RequestMapping(value = \"page\",method = RequestMethod.GET)\r\n");
            string strFunction = string.Format("\tpublic JsonBean<PageResponse<{0}>> GetPage{0}(", modelClass);
            function += strFunction;
            function += "@Param(\"起始数目\")@RequestParam(value = \"offset\") Integer offset,\r\n";
            function += "\t"+ new string(' ',strFunction.Length - 1) + string.Format("@Param(\"每页数量\")@RequestParam(value = \"pageSize\") Integer pageSize){{\r\n");
            function += string.Format("\t\treturn new JsonBean(ErrorCodeUtil.SUCCESS, {0}.GetPage{1}(offset,pageSize));\r\n", toFirstLow(serviceClass), modelClass);
            function += "\t}";

            return function;
        }
    }
}
