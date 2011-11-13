using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoBox.Abstraction
{
    internal interface IMethodInterceptor
    {
        void Intercept(IMethodInvocation invocation);
    }
}
