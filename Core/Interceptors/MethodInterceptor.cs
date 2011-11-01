using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoBox.Abstraction;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Interceptors
{
    internal class MethodInterceptor : IMethodInterceptor
    {
        private readonly Type targetType;

        /// <summary>
        /// Initializes the <see cref="MethodInterceptor"/> class.
        /// </summary>
        /// <param name="targetType">Target type</param>
        public MethodInterceptor(Type  targetType)
        {
            this.targetType = targetType;   
        }

        public void Intercept(IMethodInvocation invocation)
        {
            object target = ServiceLocator.Current.GetInstance(targetType);

            IHandler handler = MethodHandler.Resolve(invocation.Method, invocation.Arguments);

            invocation.SetReturn(handler.Invoke(target, invocation));
        }
    }
}
