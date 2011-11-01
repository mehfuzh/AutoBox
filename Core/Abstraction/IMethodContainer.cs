using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    public interface IMethodContainer
    {
        IMethodMetaData CreateMethodMetaData(System.Reflection.MethodInfo methodInfo, Argument[] arguments);
        IMethodMetaData GetMethodMetaData(System.Reflection.MethodInfo methodInfo, object[] arguments);
    }
}
