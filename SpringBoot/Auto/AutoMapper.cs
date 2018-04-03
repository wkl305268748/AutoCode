using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;

namespace SpringBoot.Auto
{
    public class AutoMapper : AutoClass
    {

        public static void CreateMapper(DbTable table, string package, string path)
        {
            string model_class = toFirstUp(table.getClassName());
            string model_value = toFirstLow(table.getClassName());

            //创建Mapper类
            JavaClass javaClass = new JavaClass(toFirstUp(table.getClassName() + "Mapper"), package, true);
            //头文件
            javaClass.AddImpoert("org.apache.ibatis.annotations.*")
                     .AddImpoert("java.util.List")
                     .AddImpoert(string.Format("{0}.model.{1}",package,model_class));
            //添加注解
            javaClass.AddAnnotation("@Mapper");

            //insert函数
            string columns = "";
            string values = "";
            string column_valus = "";
            int index = 0;
            string fk_column_valus = "";
            int fk_index = 0;
            foreach (DbColumn col in table.Column) {
                if (!col.IsKey)
                {
                    if (index == 0)
                    {
                        columns += col.Name;
                        values += string.Format("#{{{0}}}", col.Name);
                        column_valus += string.Format("{0}=#{{{0}}}", col.Name);
                    }
                    else
                    {
                        columns += "," + col.Name;
                        values += string.Format(",#{{{0}}}", col.Name);
                        column_valus += string.Format(",{0}=#{{{0}}}", col.Name);
                    }
                    index++;
                }

                //if (col.IsFkey) {
                //    if (fk_index != 0)
                //        fk_column_valus += ",";
                //    fk_column_valus += string.Format("@Result(property=\"{0}\",column=\"{1}\",one = @One(select=\"com.avatarcn.syllabus.mapper.{2}Mapper.selectByPrimaryKey\")),", col.getFkClassLowName(), col.Name, col.getFkClassName());
                //    fk_column_valus += string.Format("@Result(property=\"{0}\",column=\"{0}\")", col.Name);
                //    fk_index++;
                //}
            }

            //insert函数
            javaClass.AddInterfaceMethord("insert", "int")
                .addAnnotation(string.Format("@Insert(\"INSERT INTO {0}({1}) VALUES({2})\")", table.Name, columns, values))
                .addAnnotation(string.Format("@Options(useGeneratedKeys = true, keyProperty = \"{0}\")", table.getColumnKey().Name))
                .addParam(model_class, model_value);

            //update函数
            javaClass.AddInterfaceMethord("update", "int")
                .addAnnotation(string.Format("@Update(\"UPDATE {0} SET {1} WHERE {2}=#{{{2}}}\")", table.Name, column_valus, table.getColumnKey().Name))
                .addParam(model_class, model_value);

            //select函数
            javaClass.AddInterfaceMethord("selectByPrimaryKey", model_class)
                .addAnnotation(string.Format("@Select(\"SELECT * FROM {0} WHERE {1}=#{{{1}}}\")", table.Name, table.getColumnKey().Name))
                //.addAnnotation(string.Format("@Results({{{0}}})", fk_column_valus))
                .addParam(string.Format("@Param(value = \"{0}\")", table.getColumnKey().Name), table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //selectPage函数
            javaClass.AddInterfaceMethord("selectPage", string.Format("List<{0}>", model_class))
                .addAnnotation(string.Format("@Select(\"SELECT * FROM {0} limit #{{offset}},#{{pageSize}}\")", table.Name))
                //.addAnnotation(string.Format("@Results({{{0}}})", fk_column_valus))
                .addParam("@Param(value = \"offset\")", "Integer", "offset")
                .addParam("@Param(value = \"pageSize\")", "Integer", "pageSize");

            //count函数
            javaClass.AddInterfaceMethord("count", "int")
                .addAnnotation(string.Format("@Select(\"SELECT COUNT(*) FROM {0}\")", table.Name));

            //delete函数
            javaClass.AddInterfaceMethord("deleteByPrimaryKey", "int")
                .addAnnotation(string.Format("@Delete(\"DELETE FROM {0} WHERE {1}=#{{{1}}}\")", table.Name, table.getColumnKey().Name))
                .addParam(string.Format("@Param(value = \"{0}\")", table.getColumnKey().Name), table.getColumnKey().getJavaTyep(), table.getColumnKey().Name);

            //输出Mapper
            WriteFile(path + "\\mapper", toFirstUp(table.getClassName() + "Mapper") + ".java", javaClass.toListString());

        }
    }
}
 