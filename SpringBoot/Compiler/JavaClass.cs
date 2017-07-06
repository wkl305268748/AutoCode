using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaClass
    {
        List<JavaMethod> mJavaMethod;
        List<string> mImport;
        List<string> mAnnotation;
        string mPackage;
        string mName;
        int indent = 0;


        public JavaClass(string name,string package) {
            this.mName = name;
            this.mPackage = package;
            mJavaMethod = new List<JavaMethod>();
            mImport = new List<string>();
            mAnnotation = new List<string>();
        }

        /// <summary>
        /// 增加引用
        /// </summary>
        /// <param name="import"></param>
        public void AddImpoert(string import) {
            mImport.Add(import);
        }

        /// <summary>
        /// 增加注解
        /// </summary>
        /// <param name="annotation"></param>
        public void AddAnnotation(string annotation) {
            mAnnotation.Add(annotation);
        }

        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="methords"></param>
        public JavaMethod CreateMethord(string name,string mreturn) {
            JavaMethod method = new JavaMethod(name, mreturn, indent + 1);
            mJavaMethod.Add(method);
            return method;
        }

        public List<string> toListString()
        {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            //包名
            result.Add(mIndent + string.Format("package {0};",mPackage));
            result.Add("");
            //引用
            foreach (string import in mImport) {
                result.Add(mIndent + string.Format("import {0};",import));
            }
            result.Add("");
            //注解
            foreach (string ann in mAnnotation)
            {
                result.Add(mIndent + ann);
            }
            //类开头
            result.Add(mIndent + string.Format("public class {0}{{", mName));
            //类内容，各个函数
            foreach (JavaMethod code in mJavaMethod) { 
                result.AddRange(code.toListString());
                result.Add("");
            }
            //类结束
            result.Add(mIndent + "}");
            return result;
        }
    }
}
 