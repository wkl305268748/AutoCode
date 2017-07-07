using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaLogicException : IJavaLogic
    {
        List<string> mTry;
        List<string> mCatch;
        string catch_class;
        int indent;

        /// <summary>
        /// 创建异常语句
        /// </summary>
        /// <param name="mIf">判断条件</param>
        /// <param name="isElse">是否有else</param>
        public JavaLogicException(string catch_class,int indent)
        {
            mTry = new List<string>();
            mCatch = new List<string>();
            this.indent = indent;
            this.catch_class = catch_class;
        }

        /// <summary>
        /// 添加一个Try语句
        /// </summary>
        /// <param name="code"></param>
        public void addTryCode(string code)
        {
            mTry.Add(code);
        }

        /// <summary>
        /// 添加一个Catch语句
        /// </summary>
        /// <param name="code"></param>
        public void addCatchCode(string code)
        {
            mCatch.Add(code);
        }

        /// <summary>
        /// 输出语句list
        /// </summary>
        /// <returns></returns>
        public List<string> toListString()
        {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            string mIndentIn = new string('\t', indent + 1);
            result.Add(mIndent + "try{");
            foreach (string code in mTry)
            {
                result.Add(mIndentIn + code);
            }
            result.Add(mIndent + string.Format("}}catch({0} e){{", catch_class));

            foreach (string code in mCatch)
            {
                result.Add(mIndentIn + code);
            }
            result.Add(mIndent + "}");


            return result;
        }

    }
}
