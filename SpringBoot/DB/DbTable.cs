﻿using System;
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

        string notes;

        List<DbColumn> column = new List<DbColumn>();

        internal List<DbColumn> Column
        {
            get { return column; }
            set { column = value; }
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

        /// <summary>
        /// 获取表对应的Java类名称
        /// </summary>
        /// <returns></returns>
        public string getClassName() {
            List<string> begin = new List<string>();
            begin.Add("tb_");

            foreach (string str in begin) {
                if (name.IndexOf(str) == 0) {
                    name = name.Remove(0, str.Length);
                    break;
                }
            }
            string[] split = name.Split('_');
            string result = "";
            foreach (string sp in split) {
                result += sp[0].ToString().ToUpper() + sp.Remove(0, 1);
            }
            return result;
        }

        /// <summary>
        /// 获取表对应的小写Java类名称
        /// </summary>
        /// <returns></returns>
        public string getClassLowName()
        {
            List<string> begin = new List<string>();
            begin.Add("tb_");

            foreach (string str in begin)
            {
                if (name.IndexOf(str) == 0)
                {
                    return name.Remove(0, str.Length);
                }
            }
            return name;
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <returns></returns>
        public DbColumn getColumnKey() {
            foreach (DbColumn col in column) {
                if (col.IsKey)
                    return col;
            }
            return null;
        }
    }
}
