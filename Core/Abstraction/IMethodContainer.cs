using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    internal interface IMethodContainer
    {
        IMethod Create(System.Reflection.MethodInfo methodInfo, Argument[] arguments);
        IConfigurationItemImpl Get(System.Reflection.MethodInfo methodInfo, object[] arguments);
    }
}
