using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaMethod
    {
        string mName;
        string mReturn;
        List<string> mParam;
        List<string> mMethodCode;
        List<string> mAnnotation;
        bool isAnnotation;

        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="mname">函数名</param>
        /// <param name="mreturn">返回类型</param>
        public JavaMethod(string mname, string mreturn) {
            this.mName = mname;
            this.mReturn = mreturn;
            mParam = new List<string>();
            mMethodCode = new List<string>();
            mAnnotation = new List<string>();
            this.isAnnotation = false;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="mType"></param>
        /// <param name="mName"></param>
        public void addParam(string mType,string mName){
            mParam.Add(mType + " " + mName);
        }

        /// <summary>
        /// 添加一个带注解的参数
        /// </summary>
        /// <param name="mAnnotation"></param>
        /// <param name="mType"></param>
        /// <param name="mName"></param>
        public void addParam(string mAnnotation,string mType, string mName) {
            mParam.Add(mAnnotation + mType + " " + mName);
            isAnnotation = true;
        }

        /// <summary>
        /// 添加函数注解
        /// </summary>
        /// <param name="annotation"></param>
        public void addAnnotation(string annotation) {
            mAnnotation.Add(annotation);
        }

        /// <summary>
        /// 添加函数内部语句
        /// </summary>
        /// <param name="code"></param>
        public void addMethodCode(string code){
            mMethodCode.Add(code);
        }

        /// <summary>
        /// 添加函数内部语句List
        /// </summary>
        /// <param name="codes"></param>
        public void addMethodCodes(List<string> codes) {
            mMethodCode.AddRange(codes);
        }

        
        public List<string> toListString(int indent) {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            string mIndentIn = new string('\t', ++indent);
            //注解
            foreach (string ann in mAnnotation) {
                result.Add(mIndent + ann);
            }
            //函数
            //无注解
            if (isAnnotation)
            {
                string param = "";
                for (int i = 0; i < mParam.Count; i++)
                {
                    if (i == 0)
                    {
                        param += mParam[i];
                        continue;
                    }
                    param += "," + mParam[i];
                }
                result.Add(mIndent + string.Format("public {0}{1}({2}){{", mReturn, mName, param));
            }
            else
            {
                //有注解
                int length = string.Format("public {0}{1}(", mReturn, mName).Length;
                result.Add(mIndent + string.Format("public {0}{1}({2},", mReturn, mName,mParam[0]));

                for (int i = 1; i < mParam.Count; i++)
                {
                    if (i == mParam.Count - 1) {
                        result.Add(mIndent + new string(' ', length) + mParam[i] + "){");
                        break;
                    }
                    result.Add(mIndent + new string(' ', length) + mParam[i] + ",");
                }
            }
            //内部语句
            foreach (string code in mMethodCode) {
                result.Add(mIndentIn + code);
            }
            result.Add(mIndent + "}");
            return result;
        }
    }
}
