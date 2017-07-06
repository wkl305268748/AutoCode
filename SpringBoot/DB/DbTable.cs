using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.DB
{
    public class DbTable
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        List<DbColumn> column = new List<DbColumn>();

        internal List<DbColumn> Column
        {
            get { return column; }
            set { column = value; }
        }

        public string getClassName() {
            List<string> begin = new List<string>();
            begin.Add("tb_");

            foreach (string str in begin) {
                if (name.IndexOf(str) == 0) {
                    return name.Remove(0, str.Length);
                }
            }
            return name;
        }
    }
}
