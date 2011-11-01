using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Abstraction;
using AutoBox.Interceptors;

namespace AutoBox
{
    internal class ProxyGenerator : IInterceptorSettings
    {
        /// <summary>
        /// Initializes the new instance of  <see cref="ProxyGenerator"/> class.
        /// </summary>
        /// <param name="interceptor">Target method interceptor to invoke.</param>
        public ProxyGenerator(IMethodInterceptor interceptor)
        {
            this.interceptor = interceptor;
        }

        /// <summary>
        /// Creates the proxy for the target type.
        /// </summary>
        public object Create(Type targetType)
        {
            var proxy =  new Castle.DynamicProxy.ProxyGenerator();
            return proxy.CreateInterfaceProxyWithoutTarget(targetType, new ProxiedMethodInterceptor(interceptor));
        }

        private readonly IMethodInterceptor interceptor;
    }
}
