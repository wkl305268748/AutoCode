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
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\" + getControllerName(tableName) + ".java";

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(false);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);

            //写入文件
            //包名
            writer.WriteLine(getPackAge(pageName + ".controller"));
            writer.WriteLine();
            //引入头文件
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
            writer.WriteLine("@Api(value = \"/v1/{0}\", description = \"{0}模块\")", getUrlName(tableName));
            writer.WriteLine("@RequestMapping(value = \"/v1/{0}\")", getUrlName(tableName));
            writer.WriteLine("@RestController");
            writer.WriteLine(getClass(getControllerName(tableName), GetClassInfo(tableName, getServiceName(tableName), getClassName(tableName), field)));
            writer.Close();
        }

        private static string GetClassInfo(string table, string serviceClass, string modelClass, List<Field> fields)
        {
            string result = "\r\n";
            result += GetVariable(serviceClass) + "\r\n\r\n";
            result += AddMethod(modelClass, serviceClass, fields) + "\r\n\r\n";
            result += GetMethod(modelClass, serviceClass, fields) + "\r\n\r\n";
            result += GetPageMethod(modelClass, serviceClass) + "\r\n\r\n";
            result += EditMethod(modelClass, serviceClass, fields) + "\r\n\r\n";
            result += GetDeleteMethod(modelClass, serviceClass, fields) + "\r\n\r\n";
            
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
            function += string.Format("\t@RequestMapping(value = \"/page\",method = RequestMethod.GET)\r\n");
            function += string.Format("\t@ResponseBody\r\n");
            string strFunction = string.Format("\tpublic JsonBean<PageResponse<{0}>> GetPage{0}(", modelClass);
            function += strFunction;
            function += "@RequestParam(value = \"offset\") Integer offset,\r\n";
            function += "\t" + new string(' ', strFunction.Length - 1) + string.Format("@RequestParam(value = \"pageSize\") Integer pageSize){{\r\n");
            function += string.Format("\t\treturn new JsonBean(ErrorCode.SUCCESS, {0}.GetPage{1}(offset,pageSize));\r\n", toFirstLow(serviceClass), modelClass);
            function += "\t}";

            return function;
        }

        private static string GetDeleteMethod(string modelClass, string serviceClass, List<Field> fields)
        {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }
            string function = "";
            function += string.Format("\t@ApiOperation(value = \"删除指定的\")\r\n", modelClass);
            function += string.Format("\t@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.DELETE)\r\n", key.Name);
            function += string.Format("\t@ResponseBody\r\n");
            function += string.Format("\tpublic JsonBean Delete{0}(@PathVariable {1} {2}) {{\r\n", modelClass, key.getJavaTyep(), key.Name);
            function += string.Format("\t\treturn new JsonBean(ErrorCode.SUCCESS, {0}.Delete{1}({2}));\r\n", toFirstLow(serviceClass), modelClass, key.Name);
            function += "\t}";
            return function;
        }

        private static string GetMethod(string modelClass, string serviceClass, List<Field> fields)
        {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }
            string function = "";
            function += string.Format("\t@ApiOperation(value = \"获取指定的\")\r\n", modelClass);
            function += string.Format("\t@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.GET)\r\n", key.Name);
            function += string.Format("\t@ResponseBody\r\n");
            function += string.Format("\tpublic JsonBean<{0}> Get{0}(@PathVariable {1} {2}) {{\r\n", modelClass, key.getJavaTyep(), key.Name);
            function += "\t\ttry{\r\n";
            function += string.Format("\t\t\treturn new JsonBean(ErrorCode.SUCCESS, {0}.Get{1}({2}));\r\n", toFirstLow(serviceClass), modelClass, key.Name);
            function += "\t\t} catch (ErrorCodeException e) {\r\n";
            function += "\t\t\treturn new JsonBean(e.getErrorCode());\r\n";
            function += "\t\t}\r\n";

            function += "\t}";
            return function;
        }

        private static string AddMethod(string modelClass, string serviceClass, List<Field> fields)
        {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }
            string function = "";
            function += string.Format("\t@ApiOperation(value = \"增加{0}\")\r\n", modelClass);
            function += string.Format("\t@RequestMapping(value = \"\",method = RequestMethod.POST)\r\n", key.Name);
            function += string.Format("\t@ResponseBody\r\n");
            string strFunction = string.Format("\tpublic JsonBean<{0}> Add{0}(", modelClass, key.getJavaTyep(), key.Name);
            function += strFunction;
            string getM = "";
            int i = 0;
            foreach (Field field in fields) {

                if (field.Name.Equals("time"))
                    continue;
                if (!field.IsKey) {
                    getM += string.Format(",{0}", field.Name);
                    if (i == 0)
                        function += string.Format("@RequestParam(value = \"{0}\") {1} {0},\r\n", field.Name, field.getJavaTyep());
                    else
                        function += "\t" + new string(' ', strFunction.Length - 1) + string.Format("@RequestParam(value = \"{0}\") {1} {0},\r\n", field.Name, field.getJavaTyep());
                    i++;
                }
            }
            function = function.Remove(function.Length - 3, 3);
            getM = getM.Remove(0, 1);
            function += ") {\r\n";
            function += string.Format("\t\treturn new JsonBean(ErrorCode.SUCCESS, {0}.Add{1}({2}));\r\n", toFirstLow(serviceClass), modelClass, getM);
            function += "\t}";
            return function;
        }

        private static string EditMethod(string modelClass, string serviceClass, List<Field> fields)
        {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }
            string function = "";
            function += string.Format("\t@ApiOperation(value = \"修改指定的{0}\")\r\n", modelClass);
            function += string.Format("\t@RequestMapping(value = \"/{{{0}}}\",method = RequestMethod.PUT)\r\n", key.Name);
            function += string.Format("\t@ResponseBody\r\n");
            string strFunction = string.Format("\tpublic JsonBean<{0}> Edit{0}(", modelClass, key.getJavaTyep(), key.Name);
            function += strFunction;
            function += string.Format("@PathVariable {0} {1},\r\n", key.getJavaTyep(), key.Name);
            string getM = key.Name;
            foreach (Field field in fields)
            {
                if (field.Name.Equals("time"))
                    continue;
                if (!field.IsKey)
                {
                    getM += string.Format(",{0}", field.Name);
                    function += "\t" + new string(' ', strFunction.Length - 1) + string.Format("@RequestParam(value = \"{0}\") {1} {0},\r\n", field.Name, field.getJavaTyep());
                }
            }
            function = function.Remove(function.Length - 3, 3);
            function += ") {\r\n";
            function += "\t\ttry{\r\n";
            function += string.Format("\t\t\treturn new JsonBean(ErrorCode.SUCCESS, {0}.Edit{1}({2}));\r\n", toFirstLow(serviceClass), modelClass, getM);
            function += "\t\t} catch (ErrorCodeException e) {\r\n";
            function += "\t\t\treturn new JsonBean(e.getErrorCode());\r\n";
            function += "\t\t}\r\n";

            function += "\t}";
            return function;
        }

    }
}
