using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using AutoBox.Ast;
using System.Linq.Expressions;

namespace AutoBox
{
    /// <summary>
    /// Defines various config setting items.
    /// </summary>
    internal class ConfigurationItem<T> : IConfigurationItem<T>, IConfigurationItemImpl, ICacheConfiguration<T>
    {
        public ConfigurationItem(Configuration configuration, IMethod method)
        {
            this.configuration = configuration;
            this.method = method;
            this.itemsToInValidate = new List<IConfigurationItemImpl>();
        }

        /// <summary>
        /// Gets the cache duration defined for the calling method.
        /// </summary>
        public TimeSpan CacheDuration
        {
            get
            {
                return cacheDuration;
            }
        }

        /// <summary>
        /// Gets a value indicating the calling method is invalidated.
        /// </summary>
        public bool InValidated
        {
            get
            {
                return inValidated;
            }
            set
            {
                inValidated = value;
            }
        }

        /// <summary>
        /// Gets the value indicating that argument validation should be skipped.
        /// </summary>
        public bool IgnoreArgumentValidation 
        {
            get
            {
                return ignoreArgumentValidation;
            }
        }

        /// <summary>
        /// Gets the resolved method.
        /// </summary>
        public IMethod Method
        {
            get
            {
                return method;
            }
        }

        /// <summary>
        /// Marks the member to invalidate its cache duration.
        /// </summary>
        public void InvalidateDependencies()
        {
            itemsToInValidate.ToList().ForEach(x => x.InValidated = true);
        }

        /// <summary>
        /// Invalides the cache for the specific call.
        /// </summary>
        public IConfiguration Invalidates(Expression<Func<T, object>> expression)
        {
            var methodVisitor = new MethodVisitor();

            methodVisitor.Visit(expression);

            var item = configuration.GetConfigItem(new MethodMetaData(methodVisitor.Method, methodVisitor.Arguments));

            itemsToInValidate.Add(item);

            return configuration;
        }

        /// <summary>
        /// Specifies the cache duration.
        /// </summary>
        public ICacheConfiguration<T> Caches(TimeSpan cacheDuration)
        {
            this.cacheDuration = cacheDuration;
            return this as ICacheConfiguration<T>;
        }

        /// <summary>
        /// Specifies that the cache is variable to calling arguments.
        /// </summary>
        public IConfiguration VaryByArgs()
        {
            ignoreArgumentValidation = true;
            // TODO : include logic variable arg caching.
            return configuration;
        }

        private TimeSpan cacheDuration;
        private bool inValidated;
        private bool ignoreArgumentValidation;
        private IMethod method;

        private readonly Configuration configuration;
        IList<IConfigurationItemImpl> itemsToInValidate;
    }
}
