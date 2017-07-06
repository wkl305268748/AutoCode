using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.DB
{
    public class DataBase
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        List<DbTable> tables;

        internal List<DbTable> Tables
        {
            get { return tables; }
            set { tables = value; }
        }
    }
}
