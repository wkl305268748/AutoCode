using SpringBoot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpringBoot.Code
{
    //生成Model类
    public class MapperHelp : ClassHelp
    {
        public static void CreateMapper(string path,string pageName,string tableName,List<Field> field) {
            

            //创建文件夹
            string pathDir = path + "\\" + "mapper";
            if (!Directory.Exists(pathDir)){
                Directory.CreateDirectory(pathDir);
            }
            string filePath = pathDir + "\\" + getMapperName(tableName) + ".java";

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(true);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);

            //写入文件
            //包名
            writer.WriteLine(getPackAge(pageName+".mapper"));
            writer.WriteLine();
            //引入头文件
            writer.WriteLine(getImport("java.util.List"));
            writer.WriteLine(getImport("org.mapstruct.Mapper"));
            writer.WriteLine(getImport("org.apache.ibatis.annotations.*"));
            writer.WriteLine(getImport(pageName+".model.*"));
            writer.WriteLine();
            //引入注释
            writer.WriteLine(getNotes("Created by AutoCode on " + DateTime.Now.ToShortDateString()));
            //引入类
            writer.WriteLine("@Mapper");
            writer.WriteLine(getInterfaceClass(getMapperName(tableName), GetClassInfo(tableName,getClassName(tableName),field)));
            writer.Close();
        }

        private static string GetClassInfo(string table,string modelClass,List<Field> fields)
        {
            string result = "\r\n";
            result += GetInsert(table, modelClass, fields) + "\r\n";
            result += GetUpdate(table, modelClass, fields) + "\r\n";
            result += GetSelectByPrimaryKey(table,modelClass, fields) + "\r\n";
            result += GetDeleteByPrimaryKey(table, fields) + "\r\n";
            result += GetPage(table,modelClass) + "\r\n";
            result += GetCount(table);
            return result;
        }
        private static string GetCount(string table) {
            string sql = "\t@Select(\"SELECT COUNT(*) FROM {0}\")\r\n";
            string method = "\tint count();\r\n";
            string resutl = "";
            resutl += string.Format(sql, table);
            resutl += method;
            return resutl;
        }

        private static string GetPage(string table,string modelClass) {
            string sql = "\t@Select(\"SELECT * FROM {0} limit #{{offset}},#{{pageSize}}\")\r\n";
            string method = "\tList<{0}> selectPage(@Param(value = \"offset\") Integer offset, @Param(value = \"pageSize\") Integer pageSize);\r\n";
            string resutl = "";
            resutl += string.Format(sql, table);
            resutl += string.Format(method, modelClass); ;
            return resutl;
        }

        private static string GetDeleteByPrimaryKey(string table, List<Field> fields)
        {
            string primary = "";
            foreach (Field field in fields) {
                if (field.IsKey)
                    primary = field.Name;
            }
            string sql = "\t@Delete(\"DELETE FROM {0} WHERE {1}=#{{{1}}}\")\r\n";
            string method = "\tint deleteByPrimaryKey(@Param(value = \"{0}\") Integer {0});\r\n";
            string resutl = "";
            resutl += string.Format(sql, table, primary);
            resutl += string.Format(method,primary);
            return resutl;
        }

        private static string GetSelectByPrimaryKey(string table,string modelName, List<Field> fields)
        {
            string primary = "";
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    primary = field.Name;
            }
            string sql = "\t@Select(\"SELECT* FROM {0} WHERE {1}=#{{{1}}}\")\r\n";
            string method = "\t{0} selectByPrimaryKey(@Param(value = \"{1}\") Integer {1});\r\n";
            string resutl = "";
            resutl += string.Format(sql, table, primary);
            resutl += string.Format(method, modelName, primary);
            return resutl;
        }

        private static string GetInsert(string table, string modelName, List<Field> fields)
        {
            //主键
            string primary = "";
            string tables = "";
            string values = "";
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    primary = field.Name;
                else {
                    tables += "," + field.Name;
                    values += "," + "#{" + field.Name + "}";
                }
            }
            tables = tables.Remove(0, 1);
            values = values.Remove(0, 1);

            string sql = "\t@Insert(\"INSERT INTO {0}({1}) VALUES({2})\")\r\n";
            string sql2 = "\t@Options(useGeneratedKeys = true, keyProperty = \"{0}\")\r\n";
            string method = "\tint insert({0} {1});\r\n";
            string resutl = "";
            resutl += string.Format(sql, table, tables,values);
            resutl += string.Format(sql2, primary);

            //
            string lowModelName = modelName[0].ToString().ToLower() + modelName.Remove(0, 1);
            resutl += string.Format(method, modelName, lowModelName);
            return resutl;
        }

        private static string GetUpdate(string table, string modelName, List<Field> fields)
        {
            //主键
            string primary = "";
            string updates = "";
            foreach (Field field in fields)
            {
                if (field.IsKey)
                    primary = field.Name;
                else
                {
                    updates += "," + field.Name + "=#{" + field.Name + "}";
                }
            }
            updates = updates.Remove(0, 1);

            string sql = "\t@Update(\"UPDATE {0} SET {1} WHERE {2}=#{{{2}}}\")\r\n";
            string method = "\tint update({0} {1});\r\n";
            string resutl = "";
            resutl += string.Format(sql, table, updates, primary);
            //
            string lowModelName = modelName[0].ToString().ToLower() + modelName.Remove(0, 1);
            resutl += string.Format(method, modelName, lowModelName);
            return resutl;
        }

    }
}
