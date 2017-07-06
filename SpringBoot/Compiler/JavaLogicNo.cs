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
        /// 创建if语句
        /// </summary>
        /// <param name="mIf">判断条件</param>
        /// <param name="isElse">是否有else</param>
        public JavaLogicNo(string code,int indent)
        {
            this.indent = indent;
            codes = new List<string>();
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
