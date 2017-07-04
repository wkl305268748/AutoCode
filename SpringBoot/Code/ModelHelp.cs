using SpringBoot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpringBoot.Code
{
    //生成Model类
    public class ModelHelp : ClassHelp
    {
        private static string Variable = "\t{0} {1};";
        private static string MethodGet = "\tpublic {0} get{1}() {{\r\n\t\treturn {2};\r\n\t}}";
        private static string MethodSet = "\tpublic void set{1}({0} {2}) {{\r\n\t\tthis.{2} = {2};\r\n\t}}";
        public static void CreateModel(string path,string pageName,string tableName,List<Field> field) {
            

            //创建文件夹
            string pathDir = path + "\\" + "model";
            if (!Directory.Exists(pathDir)){
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\" + getClassName(tableName) + ".java";

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);

            //写入文件
            //包名
            writer.WriteLine(getPackAge(pageName+".model"));
            writer.WriteLine();
            //引入头文件
            writer.WriteLine(getImport("java.util.Date"));
            writer.WriteLine();
            //引入注释
            writer.WriteLine(getNotes("Created by AutoCode on " + DateTime.Now.ToShortDateString()));
            //引入类
            writer.WriteLine(getClass(getClassName(tableName), GetClassInfo(field)));
            writer.Close();
        }

        private static string GetClassInfo(List<Field> fields) {
            string result = "";
            result += GetAllVariable(fields) + "\r\n";
            result += GetAllMethod(fields);
            return result;
        }

        private static string GetAllMethod(List<Field> fields) {
            string result ="";
            foreach (Field field in fields) {
                result += GetMethodGet(field) + "\r\n\r\n";
                result += GetMethodSet(field) + "\r\n\r\n";
            }
            return result;
        }

        private static string GetMethodGet(Field field) {
            return string.Format(MethodGet, field.getJavaTyep(), field.getMethodName(), field.getVariableName());
        }

        private static string GetMethodSet(Field field)
        {
            return string.Format(MethodSet, field.getJavaTyep(), field.getMethodName(), field.getVariableName());
        }

        private static string GetAllVariable(List<Field> fields) {
            string result = "";
            foreach (Field field in fields)
            {
                result += GetVariable(field) + "\r\n";
            }
            return result;
        }

        private static string GetVariable(Field field)
        {
            return string.Format(Variable, field.getJavaTyep(), field.getVariableName());
        }
    }
}
