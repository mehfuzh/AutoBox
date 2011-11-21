using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Abstraction;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Ast;
using AutoBox.Attributes;

namespace AutoBox
{
    /// <summary>
    /// Entry-point for defining settings for a resolved member.
    /// </summary>
    [NoIntercept]
    public sealed class Configuration : IConfiguration
    {
        /// <summary>
        /// Initializes the new instance of <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            config = new Dictionary<string, IConfigurationItemImpl>();
        }

        /// <summary>
        /// Gets the config item for the target method.
        /// </summary>
        /// <param name="methodMetaData">Defines the method metadata</param>
        /// <returns></returns>
        internal IConfigurationItemImpl GetConfigItem(IMethod methodMetaData)
        {
            if (methodMetaData != null && config.ContainsKey(methodMetaData.Key))
                return config[methodMetaData.Key];
            return null;
        }

        /// <summary>
        /// Defines the configuration of the resolved method.
        /// </summary>
        public IConfigurationItem<T> Setup<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            var methodVisitor = new MethodVisitor();

            methodVisitor.Visit(expression);

            return CreateConfigurationItem<T>(methodVisitor);
        }

        /// <summary>
        /// Defines the configuration of the resolved method.
        /// </summary>
        public IConfigurationItem<T> Setup<T>(System.Linq.Expressions.Expression<Action<T>> expression)
        {
            var methodVisitor = new MethodVisitor();

            methodVisitor.Visit(expression);

            return CreateConfigurationItem<T>(methodVisitor);
        }

        private IConfigurationItem<T> CreateConfigurationItem<T>(MethodVisitor visitor)
        {
            var methodContainer = ServiceLocator.Current.GetInstance<IMethodContainer>();

            IMethod method = methodContainer.Create(visitor.Method, visitor.Arguments);

            if (!config.ContainsKey(method.Key))
                config.Add(method.Key, new ConfigurationItem<T>(this, method));
         
            return config[method.Key] as IConfigurationItem<T>;
        }

        readonly IDictionary<string, IConfigurationItemImpl> config;
    }
}
