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
        List<string> mValue;
        string mPackage;
        string mName;
        int indent = 0;
        bool isInterface;


        public JavaClass(string name,string package) {
            this.mName = name;
            this.mPackage = package;
            mJavaMethod = new List<JavaMethod>();
            mImport = new List<string>();
            mAnnotation = new List<string>();
            mValue = new List<string>();
            isInterface = false;
        }

        public JavaClass(string name, string package, bool isInterface) : this(name, package){
            isInterface = true;
        }

        /// <summary>
        /// 增加引用
        /// </summary>
        /// <param name="import"></param>
        public JavaClass AddImpoert(string import) {
            mImport.Add(import);
            return this;
        }

        /// <summary>
        /// 增加注解
        /// </summary>
        /// <param name="annotation"></param>
        public JavaClass AddAnnotation(string annotation) {
            mAnnotation.Add(annotation);
            return this;
        }

        /// <summary>
        /// 添加变量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public JavaClass AddValue(string type,string value) {
            mValue.Add(string.Format("private {0} {1};", type, value));
            return this;
        }

        /// <summary>
        /// 添加带注解变量
        /// </summary>
        /// <param name="annotation"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public JavaClass AddValue(string annotation,string type,string value)
        {
            mValue.Add(annotation);
            mValue.Add(string.Format("private {0} {1};", type, value));
            return this;
        }

        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="methords"></param>
        public JavaMethod AddMethord(string name,string mreturn) {
            JavaMethod method = new JavaMethod(name, mreturn, indent + 1);
            mJavaMethod.Add(method);
            return method;
        }

        /// <summary>
        /// 创建接口函数
        /// </summary>
        /// <param name="methords"></param>
        public JavaMethod AddInterfaceMethord(string name, string mreturn)
        {
            JavaMethod method = new JavaMethod(name, mreturn, indent + 1, true);
            mJavaMethod.Add(method);
            return method;
        }

        public List<string> toListString()
        {
            List<string> result = new List<string>();
            //增加缩进
            string mIndent = new string('\t', indent);
            string mIndentIn = new string('\t', indent + 1);
            //包名
            result.Add(mIndent + string.Format("package {0};", mPackage));
            result.Add("");
            //引用
            foreach (string import in mImport)
            {
                result.Add(mIndent + string.Format("import {0};", import));
            }
            result.Add("");
            //注解
            foreach (string ann in mAnnotation)
            {
                result.Add(mIndent + ann);
            }

            //接口的调用方法
            if (isInterface)
            {
                result.Add(mIndent + string.Format("public interface {0}{{", mName));
            }
            else
            {
                //类开头
                result.Add(mIndent + string.Format("public class {0}{{", mName));
            }
            //类变量
            foreach (string value in mValue)
            {
                result.Add(mIndentIn + value);
            }
            result.Add("");
            //类内容，各个函数
            foreach (JavaMethod code in mJavaMethod)
            {
                result.AddRange(code.toListString());
                result.Add("");
            }
            //类结束
            result.Add(mIndent + "}");

            return result;
        }
    }
}
 