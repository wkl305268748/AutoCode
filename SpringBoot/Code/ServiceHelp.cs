using SpringBoot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpringBoot.Code
{
    //生成Service类
    public class ServiceHelp : ClassHelp
    {
        public static void CreateService(string path,string pageName,string tableName,List<Field> field) {

            CreateJson(path,pageName);
            CreateResponse(path, pageName);
            CreateErrorCode(path, pageName);
            CreateErrorCodeException(path, pageName);

            //创建文件夹
            string pathDir = path + "\\" + "service";
            if (!Directory.Exists(pathDir)){
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\" + getServiceName(tableName) + ".java";

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);

            //写入文件
            //包名
            writer.WriteLine(getPackAge(pageName+ ".service"));
            writer.WriteLine();
            //引入头文件
            writer.WriteLine(getImport("org.springframework.beans.factory.annotation.Autowired"));
            writer.WriteLine(getImport("org.springframework.stereotype.Service"));
            writer.WriteLine(getImport(pageName + ".model.*"));
            writer.WriteLine(getImport(pageName + ".mapper.*"));
            writer.WriteLine(getImport(pageName + ".json.response.*"));
            writer.WriteLine(getImport(pageName + ".exception.*"));
            writer.WriteLine(getImport("java.util.List"));
            writer.WriteLine(getImport("java.util.Date"));
            writer.WriteLine(getImport("java.util.ArrayList"));
            writer.WriteLine();
            //引入注释
            writer.WriteLine(getNotes("Created by AutoCode on " + DateTime.Now.ToShortDateString()));
            //引入类
            writer.WriteLine("@Service");
            writer.WriteLine(getClass(getServiceName(tableName), GetClassInfo(tableName, getMapperName(tableName),getClassName(tableName),field)));
            writer.Close();
        }

        private static string GetClassInfo(string table,string mapperClass, string modelClass, List<Field> fields) {
            string result = "";
            result += GetVariable(mapperClass) + "\r\n\r\n";
            result += GetPageMethod(modelClass, mapperClass, fields) + "\r\n\r\n";
            result += GetByPrimaryKeyMethod(modelClass, mapperClass, fields) + "\r\n\r\n";
            result += GetAddMethod(modelClass, mapperClass, fields) + "\r\n\r\n";
            result += GetEditMethod(modelClass, mapperClass, fields) + "\r\n\r\n";
            result += GetDeleteMethod(modelClass, mapperClass, fields) + "\r\n\r\n";
            return result;
        }

        private static string GetVariable(string mapperClass) {
            string result = "";
            result += "\t@Autowired\r\n";
            result += string.Format("\t{0} {1};\r\n", mapperClass, toFirstLow(mapperClass));
            return result;
        }
        private static string GetAddMethod(string modelClass,string mapperClass,List<Field> fields) {
            
            //初始化model
            string funciton = string.Format("\t\t{0} {1} = new {0}();\r\n", modelClass, toFirstLow(modelClass));

            string parameter = "";
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    continue;
                if (field.Name.Equals("time")) {
                    funciton += string.Format("\t\t{0}.set{1}({2});\r\n", toFirstLow(modelClass),toFirstUp(field.Name), "new Date()");
                    continue;
                }
                parameter += "," + field.getJavaTyep() + " " + field.getVariableName();
                funciton += string.Format("\t\t{0}.set{1}({2});\r\n", toFirstLow(modelClass), toFirstUp(field.Name), field.Name);
            }
            parameter = parameter.Remove(0, 1);

            //调用mapper
            funciton += string.Format("\t\treturn {0}.insert({1});\r\n", toFirstLow(mapperClass),toFirstLow(modelClass));

            //生成函数

            return string.Format("\tpublic {0} {1}({2}) throws ErrorCodeException {{\r\n{3}\r\n\t}}", modelClass,"Add"+modelClass,parameter,funciton);
        }
        private static string GetEditMethod(string modelClass, string mapperClass, List<Field> fields) {

            string key = "";
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field.Name;
            }
            string funciton = string.Format("\t\t{0} {1} = {2}.selectByPrimaryKey({3});\r\n", modelClass, toFirstLow(modelClass),toFirstLow(mapperClass),key);

            //异常抛出
            funciton += string.Format("\t\tif({0} == null)\r\n",toFirstLow(modelClass));
            funciton += string.Format("\t\t\tthrow new ErrorCodeException(ErrorCodeException.DATA_NO_ERROR);\r\n");

            string parameter = "";
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    continue;
                if (field.Name.Equals("time"))
                    continue;

                parameter += "," + field.getJavaTyep() + " " + field.getVariableName();
                funciton += string.Format("\t\t{0}.set{1}({2});\r\n", toFirstLow(modelClass), toFirstUp(field.Name), field.Name);
            }
            parameter = parameter.Remove(0, 1);

            //调用mapper
            funciton += string.Format("\t\treturn {0}.update({1});\r\n", toFirstLow(mapperClass), toFirstLow(modelClass));

            return string.Format("\tpublic {0} {1}({2}) throws ErrorCodeException {{\r\n{3}\r\n\t}}", modelClass, "Edit" + modelClass, parameter, funciton);
        }
        private static string GetByPrimaryKeyMethod(string modelClass, string mapperClass, List<Field> fields) {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }

            string funciton = string.Format("\t\t{0} {1} = {2}.selectByPrimaryKey({3});\r\n", modelClass, toFirstLow(modelClass), toFirstLow(mapperClass), key);
            //异常抛出
            funciton += string.Format("\t\tif({0} == null)\r\n", toFirstLow(modelClass));
            funciton += string.Format("\t\t\tthrow new ErrorCodeException(ErrorCodeException.DATA_NO_ERROR);\r\n");

            funciton += string.Format("\t\treturn {0};\r\n", toFirstLow(mapperClass));

            return string.Format("\tpublic {0} {1}({2} {3}) throws ErrorCodeException {{\r\n{4}\r\n\t}}", modelClass, "Get" + modelClass, key.getJavaTyep(),key.Name, funciton);

        }
        private static string GetDeleteMethod(string modelClass, string mapperClass, List<Field> fields) {
            Field key = new Field();
            //获取主键
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    key = field;
            }
            string funciton = string.Format("\t\treturn {0}.deleteByPrimaryKey({1});\r\n", toFirstLow(mapperClass), key.Name);

            return string.Format("\tpublic {0} {1}({2} {3}) throws ErrorCodeException {{\r\n{4}\r\n\t}}", "int", "Delete" + modelClass, key.getJavaTyep(), key.Name, funciton);
        }
        private static string GetPageMethod(string modelClass, string mapperClass, List<Field> fields) {

            string function = "";
            function += string.Format("\t\tPageResponse<{0}> response = new PageResponse();\r\n", modelClass);
            function += string.Format("\t\tresponse.setItem({0}.selectPage(offset,pageSize));\r\n", toFirstLow(mapperClass));
            function += string.Format("\t\tresponse.setTotal({0}.count());\r\n", toFirstLow(mapperClass));
            function += string.Format("\t\tresponse.setOffset(offset);\r\n");
            function += string.Format("\t\tresponse.setPageSize(pageSize);\r\n");
            function += string.Format("\t\treturn response;\r\n");

            return string.Format("\tpublic PageResponse<{0}> {1}(int pageSize, int offset){{\r\n{2}\r\n\t}}", modelClass, "GetPage" + modelClass, function);
        }

        private static void CreateJson(string path,string package) {
            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom
            //Json
            string pathDir = path + "\\" + "json";
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\JsonBean.java";
            string Text = Properties.Resources.JsonBean.Replace("#package#", package).Replace("#notes#", "Created by AutoCode on " + DateTime.Now.ToShortDateString());
            StreamWriter Writer = new StreamWriter(filePath, false, utf8WithBom);
            Writer.Write(Text);
            Writer.Close();
        }

        private static void CreateResponse(string path, string package)
        {
            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom

            string pathDir = path + "\\" + "json\\response";
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\PageResponse.java";
            string Text = Properties.Resources.PageResponse.Replace("#package#", package).Replace("#notes#", "Created by AutoCode on " + DateTime.Now.ToShortDateString());
            StreamWriter Writer = new StreamWriter(filePath, false, utf8WithBom);
            Writer.Write(Text);
            Writer.Close();
        }

        private static void CreateErrorCode(string path, string package)
        {
            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom

            string pathDir = path + "\\" + "exception";
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\ErrorCode.java";
            string Text = Properties.Resources.ErrorCode.Replace("#package#", package).Replace("#notes#", "Created by AutoCode on " + DateTime.Now.ToShortDateString());
            StreamWriter Writer = new StreamWriter(filePath, false, utf8WithBom);
            Writer.Write(Text);
            Writer.Close();
        }

        private static void CreateErrorCodeException(string path, string package)
        {
            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom

            string pathDir = path + "\\" + "exception";
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\ErrorCodeException.java";
            string Text = Properties.Resources.ErrorCodeException.Replace("#package#", package).Replace("#notes#", "Created by AutoCode on " + DateTime.Now.ToShortDateString());
            StreamWriter Writer = new StreamWriter(filePath, false, utf8WithBom);
            Writer.Write(Text);
            Writer.Close();
        }
    }
}
