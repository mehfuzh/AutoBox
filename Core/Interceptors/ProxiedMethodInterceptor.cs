using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using AutoBox.Abstraction;

namespace AutoBox.Interceptors
{
    internal class ProxiedMethodInterceptor : IInterceptor
    {
        public ProxiedMethodInterceptor(IMethodInterceptor interceptor)
        {
            this.interceptor = interceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodInvocation = new MethodInvocation(invocation.InvocationTarget, invocation.Method, invocation.Arguments);
            interceptor.Intercept(methodInvocation);
            invocation.ReturnValue = methodInvocation.ReturnValue;
        }

        private readonly IMethodInterceptor interceptor;
    }
}
