using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpringBoot.Compiler
{
    public class JavaClass
    {
        public enum ClassType { 
            java_class,
            java_interface
        }

        ClassType classType;
        List<>

        public JavaClass() {
            this.classType = ClassType.java_class;
        }

        public JavaClass(ClassType mclassType) {
            this.classType = mclassType;
        }

        
    }
}
