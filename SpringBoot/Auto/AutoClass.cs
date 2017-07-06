using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpringBoot.Auto
{
    public class AutoClass
    {
        protected static void WriteFile(string path, string name, List<string> codes)
        {
            //创建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = path + "\\" + name;

            //创建UTF8无BOM文件
            var utf8WithBom = new System.Text.UTF8Encoding(false);  // 用true来指定包含bom
            StreamWriter writer = new StreamWriter(filePath, false, utf8WithBom);
            foreach(string code in codes)
                writer.WriteLine(code);

            writer.Close();
        }


        protected static string toFirstUp(string name)
        {
            return name[0].ToString().ToUpper() + name.Remove(0, 1);
        }

        protected static string toFirstLow(string name)
        {
            return name[0].ToString().ToLower() + name.Remove(0, 1);
        }
    }
}
