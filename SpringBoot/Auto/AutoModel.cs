﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpringBoot.Model;
using SpringBoot.Compiler;
using SpringBoot.DB;

namespace SpringBoot.Auto
{
    public class AutoModel:AutoClass
    {

        public static void CreateModel(DbTable table, string package, string path)
        {
            //创建Model类
            JavaClass javaClass = new JavaClass(toFirstUp(table.getClassName()), package);
            //头文件
            javaClass.AddImpoert("java.util.Date");
            //添加变量
            foreach (DbColumn column in table.Column)
            {
                javaClass.AddValue(column.getJavaTyep(), column.Name);

                //外键情况
                if (column.IsFkey) {
                    javaClass.AddValue(column.getFkClassName(), column.getFkClassLowName());
                }
            }
            //添加函数
            foreach (DbColumn column in table.Column)
            {
                javaClass.AddMethord("get" + toFirstUp(column.Name), column.getJavaTyep())
                    .addLogicNo(string.Format("return {0};", column.Name));

                javaClass.AddMethord("set" + toFirstUp(column.Name), "void")
                    .addParam(column.getJavaTyep(), column.Name)
                    .addLogicNo(string.Format("this.{0} = {0};", column.Name));
            }

            WriteFile(path + "\\model_no", toFirstUp(table.getClassName()) + ".java", javaClass.toListString());

        }
    }
}
