using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaMethodIf
    {
        string mIf;
        bool isElse;
        List<string> mIfCode;
        List<string> mElseCode;

        /// <summary>
        /// 创建if语句
        /// </summary>
        /// <param name="mIf">判断条件</param>
        /// <param name="isElse">是否有else</param>
        public JavaMethodIf(string mIf) {
            this.mIf = mIf;
            this.isElse = false;
            mIfCode = new List<string>();
            mElseCode = new List<string>();
        }

        /// <summary>
        /// 添加一个if语句
        /// </summary>
        /// <param name="code"></param>
        public void addIfCode(string code){
            mIfCode.Add(code);
        }

        /// <summary>
        /// 添加一个else语句
        /// </summary>
        /// <param name="code"></param>
        public void addElseCode(string code) {
            this.isElse = true;
            mElseCode.Add(code);
        }

        /// <summary>
        /// 输出语句list
        /// </summary>
        /// <param name="indent">当前代码环境缩进量</param>
        /// <returns></returns>
        public List<string> toListString(int indent) {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            string mIndentIn = new string('\t', ++indent);
            result.Add(mIndent + string.Format("if({0}){{", mIf));
            foreach (string code in mIfCode)
            {
                result.Add(mIndentIn + code);
            }
            result.Add(mIndent + "}");

            if (isElse)
            {
                result.Add(mIndent + "else{");
                foreach (string code in mElseCode)
                {
                    result.Add(mIndentIn + code);
                }
                result.Add(mIndent + "}");
            }

            return result;
        }

    }
}
