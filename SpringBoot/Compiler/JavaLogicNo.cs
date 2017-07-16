using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaLogicNo : IJavaLogic
    {
        int indent;
        List<string> codes;

        /// <summary>
        /// 创建普通语句
        /// </summary>
        public JavaLogicNo(string code,int indent)
        {
            this.indent = indent;
            codes = new List<string>();
            codes.Add(code);
        }
        
        public List<string> toListString()
        {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            foreach (string code in codes)
            {
                result.Add(mIndent + code);
            }
            return result;
        }

    }
}
