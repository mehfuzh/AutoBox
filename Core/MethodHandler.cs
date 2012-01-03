using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using AutoBox.Locator;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Handlers;
using System.Reflection;

namespace AutoBox
{
    internal static class MethodHandler
    {
        /// <summary>
        /// Resolves the invocation handler for a specific config settings defined by user.
        /// </summary>
        public static IHandler Resolve(MethodInfo methodInfo, object[] arguments)
        {
            var methodContainer = ServiceLocator.Current.GetInstance<IMethodContainer>();
            var configItem = methodContainer.Get(methodInfo, arguments);

            IHandler handler = new BaseMethodHandler();

            if (configItem != null)
            {
                if (configItem.CacheDuration.TotalMilliseconds > 0)
                {
                    string containerId = ((AutoBoxServiceLocator) ServiceLocator.Current).TypeContainer.Id;
                    string compositeKey = string.Format("{0}+{1}", configItem.Method.Key, containerId);

                    handler = new CacheMethodHandler(compositeKey, configItem.CacheDuration, configItem.IsInvalidated, arguments);

                    if (configItem.IsInvalidated) configItem.IsInvalidated = false;
                }
                // has items to invalidate.
                configItem.InvalidateDependencies();
            }

            return handler;
        }
    }
}
