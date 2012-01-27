using System;
using System.Linq;
using AutoBox.Abstraction;
using Castle.DynamicProxy;

namespace AutoBox.Interceptors
{
    public class ProxiedMethodInterceptor<T> : IInterceptor
    {
        public ProxiedMethodInterceptor()
        {
            this.interceptor = new MethodInterceptor(typeof(T));
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
