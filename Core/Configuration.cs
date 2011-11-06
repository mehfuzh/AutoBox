﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Abstraction;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Ast;

namespace AutoBox
{
    public sealed class Configuration : IConfiguration , ISpecific
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
        internal IConfigurationItemImpl GetConfigItem(IMethodMetaData methodMetaData)
        {
            if (methodMetaData != null && config.ContainsKey(methodMetaData.Key))
                return config[methodMetaData.Key];
            return null;
        }

        public IConfigurationItem<T> Setup<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            var methodVisitor = new MethodVisitor();

            methodVisitor.Visit(expression);

            return CreateConfigurationItem<T>(methodVisitor);
        }

        public IConfigurationItem<T> Setup<T>(System.Linq.Expressions.Expression<Action<T>> expression)
        {
            var methodVisitor = new MethodVisitor();

            methodVisitor.Visit(expression);

            return CreateConfigurationItem<T>(methodVisitor);
        }

        private IConfigurationItem<T> CreateConfigurationItem<T>(MethodVisitor visitor)
        {
            var methodContainer = ServiceLocator.Current.GetInstance<IMethodContainer>();

            IMethodMetaData metaData = methodContainer.CreateMethodMetaData(visitor.Method, visitor.Arguments);

            if (!config.ContainsKey(metaData.Key))
                config.Add(metaData.Key, new ConfigurationItem<T>(this));
         
            return config[metaData.Key] as IConfigurationItem<T>;
        }

        readonly IDictionary<string, IConfigurationItemImpl> config;
    }
}