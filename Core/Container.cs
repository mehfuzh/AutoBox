using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

using Microsoft.Practices.ServiceLocation;

using AutoBox.Abstraction;
using AutoBox.Locator;
using AutoBox.Resolver.Abstraction;
using AutoBox.Resolver;
using AutoBox.Containers;

namespace AutoBox
{
    /// <summary>
    /// Entry-point for initializing model dependencies and caching.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Initializes dependencies and caching handlers for current project.
        /// </summary>
        public static void Init()
        {
            Init(new AssemblyResolver(Assembly.GetCallingAssembly()));
        }

        /// <summary>
        /// Initializes dependencies and caching handlers for an assembly.
        /// </summary>
        public static void Init(IResolver resolver)
        {
            var container = new TypeContainer(resolver.Assembly);

            container.Register(typeof(IConfiguration), typeof(Configuration));
            container.Register(typeof(IMethodContainer), typeof(MethodContainer));

            ServiceLocator.SetLocatorProvider(() => new AutoBoxServiceLocator(container));

            configuration =  (IConfiguration) ServiceLocator.Current.GetInstance(typeof(IConfiguration));
        }

        /// <summary>
        /// Specifies configuration for the target member.
        /// </summary>
        public static IConfigurationItem<T> Setup<T>(Expression<Func<T, object>> expression) where T : class
        {
            return configuration.Setup<T>(expression);
        }

        /// <summary>
        /// Specifies configuration for the target member.
        /// </summary>
        public static IConfigurationItem<T> Setup<T>(Expression<Action<T>> expression) where T : class
        {
            return configuration.Setup<T>(expression);
        }

        private static IConfiguration configuration;
    }
}
