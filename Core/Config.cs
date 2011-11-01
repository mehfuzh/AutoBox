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
    /// Entry-point for initalizing model depndencies and caching.
    /// </summary>
    public static class Box
    {
        /// <summary>
        /// Initalizes dependencies and caching handlers for current project.
        /// </summary>
        public static void Init()
        {
            Init(new AssemblyResolver(Assembly.GetCallingAssembly()));
        }

        /// <summary>
        /// Initalizes dependencies and caching handlers for specific assembly.
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
        /// Defines specific config for a particular member.
        /// </summary>
        public static IConfigurationItem<T> Setup<T>(Expression<Func<T, object>> expression) where T : class
        {
            return configuration.Setup<T>(expression);
        }

        /// <summary>
        /// Defines specific config for a particular member.
        /// </summary>
        public static IConfigurationItem<T> Setup<T>(Expression<Action<T>> expression) where T : class
        {
            return configuration.Setup<T>(expression);
        }

        private static IConfiguration configuration;
    }
}
