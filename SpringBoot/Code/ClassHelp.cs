using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpringBoot.Code
{
    public class ClassHelp
    {
        static string PackAge = "package {0};";
        static string Import = "import {0};";
        static string Notes = "/**\r\n /* {0}/\r\n */";
        static string ClassIn = "public class {0}{{\r\n{1}\r\n}}";
        static string InterfaceIn = "public interface {0}{{\r\n{1}\r\n}}";

        public static string getPackAge(string package) {
            return string.Format(PackAge, package);
        }

        public static string getImport(string import)
        {
            return string.Format(Import, import);
        }

        public static string getNotes(string notes) {
            return string.Format(Notes, notes);
        }

        public static string getClass(string className, string init) {
            return string.Format(ClassIn, className, init);
        }

        public static string getInterfaceClass(string className, string init)
        {
            return string.Format(InterfaceIn, className, init);
        }

        public static string getClassName(string tableName) {
            if (tableName.IndexOf("tb_") == 0)
                tableName = tableName.Remove(0, 3);
            string first = tableName[0].ToString().ToUpper();
            tableName = first + tableName.Remove(0, 1);

            while (tableName.IndexOf("_") >= 0) {
                int index = tableName.IndexOf("_");
                string uper = tableName[index + 1].ToString().ToUpper();
                tableName = tableName.Remove(index, 2);
                tableName = tableName.Insert(index, uper);
            }
            return tableName;
        }

        public static string getUrlName(string tableName)
        {
            if (tableName.IndexOf("tb_") == 0)
                tableName = tableName.Remove(0, 3);
            return tableName;
        }

        public static string getMapperName(string tableName) {
            return getClassName(tableName) + "Mapper";
        }

        public static string getServiceName(string tableName)
        {
            return getClassName(tableName) + "Service";
        }

        public static string getControllerName(string tableName)
        {
            return getClassName(tableName) + "Controller";
        }

        public static string toFirstUp(string name) {
            return name[0].ToString().ToUpper() + name.Remove(0, 1);
        }

        public static string toFirstLow(string name)
        {
            return name[0].ToString().ToLower() + name.Remove(0, 1);
        }
    }
}
