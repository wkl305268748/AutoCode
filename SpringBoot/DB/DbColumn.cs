using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.DB
{
    public class DbColumn
    {
        string name;
        string type;
        string lenth;
        bool isKey;
        bool isNotNull;

        string notes;

        Dictionary<string, string> sqlTojava = new Dictionary<string, string>();

        public DbColumn()
        {

            sqlTojava.Add("char", "String");
            sqlTojava.Add("varchar", "String");
            sqlTojava.Add("int", "Integer");
            sqlTojava.Add("tinyint", "Boolean");
            sqlTojava.Add("float", "Float");
            sqlTojava.Add("date", "Date");
            sqlTojava.Add("datetime", "Date");
            sqlTojava.Add("text", "String");
            sqlTojava.Add("double", "Double");
            sqlTojava.Add("bigint", "Long");
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string Lenth
        {
            get
            {
                return lenth;
            }

            set
            {
                lenth = value;
            }
        }

        public bool IsKey
        {
            get
            {
                return isKey;
            }

            set
            {
                isKey = value;
            }
        }

        public string Notes
        {
            get
            {
                return notes;
            }

            set
            {
                notes = value;
            }
        }


        public bool IsNotNull
        {
            get { return isNotNull; }
            set { isNotNull = value; }
        }


        /// <summary>
        /// 获取SQL对应的Java类型
        /// </summary>
        /// <returns></returns>
        public string getJavaTyep() {
            return sqlTojava[type];
        }

    }
}
