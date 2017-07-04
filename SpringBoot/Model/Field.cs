using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Model
{
    public class Field
    {
        string name;
        string type;
        string lenth;
        bool isKey;
        bool isNull;
        string notes;

        Dictionary<string, string> sqlTojava = new Dictionary<string, string>();

        public Field() {

            sqlTojava.Add("char", "String");
            sqlTojava.Add("varchar", "String");
            sqlTojava.Add("int", "Integer");
            sqlTojava.Add("tinyint", "Boolean");
            sqlTojava.Add("float", "Float");
            sqlTojava.Add("date", "Date");
            sqlTojava.Add("datetime", "Date");
            sqlTojava.Add("text", "String");
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

        public bool IsNull
        {
            get
            {
                return isNull;
            }

            set
            {
                isNull = value;
            }
        }

        public string getJavaTyep() {
            return sqlTojava[type];
        }

        public string getMethodName() {
            return name[0].ToString().ToUpper() + name.Remove(0, 1);
        }

        public string getVariableName()
        {
            return name;
        }
    }
}
