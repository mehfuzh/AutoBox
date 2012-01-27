using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoBox.Abstraction
{
    public interface IMethodInterceptor
    {
        void Intercept(IMethodInvocation invocation);
    }
}
