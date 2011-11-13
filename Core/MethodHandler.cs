using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Handlers;
using System.Reflection;

namespace AutoBox
{
    internal static class MethodHandler
    {
       /// <summary>
       /// Resolves the handler for the specific config item.
       /// </summary>
        public static IHandler Resolve(MethodInfo methodInfo, object[] arguments)
        {
            var configuration = (Configuration)ServiceLocator.Current.GetInstance<IConfiguration>();

            var methodConfig = ServiceLocator.Current.GetInstance<IMethodContainer>();

            IMethod metaData = methodConfig.Get(methodInfo, arguments);

            var configItem = configuration.GetConfigItem(metaData);

            IHandler handler = new BaseMethodHandler();

            if (configItem != null)
            {
                if (configItem.CacheDuration.TotalMilliseconds > 0)
                {
                    string containerId =  (ServiceLocator.Current as Locator.AutoBoxServiceLocator).TypeContainer.Id;
                    string compositeKey = string.Format("{0}+{1}", metaData.Key, containerId);

                    handler =   new CacheMethodHandler(compositeKey, configItem.CacheDuration, configItem.InValidated, arguments);

                    if (configItem.InValidated) configItem.InValidated = false;
                }
                // has items to invalidate.
                configItem.InvalidateDependencies();
            }

            return handler;
        }
    }
}
