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
        //是否是外键
        bool isFkey;
        //外键关联的表名称
        string fkTable;
        //外键关联的字段名称
        string fkColumn;

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
            sqlTojava.Add("time", "Date");
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

        public bool IsFkey
        {
            get
            {
                return isFkey;
            }

            set
            {
                isFkey = value;
            }
        }

        public string FkTable
        {
            get
            {
                return fkTable;
            }

            set
            {
                fkTable = value;
            }
        }

        public string FkColumn
        {
            get
            {
                return fkColumn;
            }

            set
            {
                fkColumn = value;
            }
        }


        /// <summary>
        /// 获取SQL对应的Java类型
        /// </summary>
        /// <returns></returns>
        public string getJavaTyep() {
            return sqlTojava[type];
        }

        /// <summary>
        /// 获取Fk表对应的Java类名称
        /// </summary>
        /// <returns></returns>
        public string getFkClassName()
        {
            string mname = fkTable;

            List<string> begin = new List<string>();
            begin.Add("tb_");

            foreach (string str in begin)
            {
                if (mname.IndexOf(str) == 0)
                {
                    mname = mname.Remove(0, str.Length);
                    break;
                }
            }
            string[] split = mname.Split('_');
            string result = "";
            foreach (string sp in split)
            {
                result += sp[0].ToString().ToUpper() + sp.Remove(0, 1);
            }
            return result;
        }

        /// <summary>
        /// 获取Fk表对应的小写Java类名称
        /// </summary>
        /// <returns></returns>
        public string getFkClassLowName()
        {
            string mname = fkTable;
            List<string> begin = new List<string>();
            begin.Add("tb_");

            foreach (string str in begin)
            {
                if (mname.IndexOf(str) == 0)
                {
                    return mname.Remove(0, str.Length);
                }
            }
            return mname;
        }

    }
}
