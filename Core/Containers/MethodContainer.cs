using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using AutoBox.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Containers
{
    /// <summary>
    /// Defines method metadata.
    /// </summary>
    [NoIntercept]
    public sealed class MethodContainer : IMethodContainer
    {
        /// <summary>
        /// Initialize the instance of <see cref="MethodContainer"/> class.
        /// </summary>
        public MethodContainer()
        {
            methods = new Dictionary<MethodHash, IList<IMethod>>();
        }

        /// <summary>
        /// Extends the specific method for a particular array of arguments to be used with the container.
        /// </summary>
        IMethod IMethodContainer.Create(System.Reflection.MethodInfo methodInfo, Argument[] arguments)
        {
            var methodHash = new MethodHash(methodInfo);

            if (!methods.ContainsKey(methodHash))
                methods.Add(methodHash, new List<IMethod>());

            var methodMetaData = new MethodMetaData(methodHash, arguments);

            methods[methodHash].Add(methodMetaData);

            return methodMetaData;
        }

        /// <summary>
        /// Gets the extended container method.
        /// </summary>
        IConfigurationItemImpl IMethodContainer.Get(System.Reflection.MethodInfo methodInfo, object[] arguments)
        {
            var methodHash = new MethodHash(methodInfo);
            var configuraiton = (Configuration) ServiceLocator.Current.GetInstance<IConfiguration>();

            if (methods.ContainsKey(methodHash))
            {
                foreach (var metaData in methods[methodHash])
                {
                    var item = configuraiton.GetConfigItem(metaData);

                    if (item.IgnoreArgumentValidation)
                        return item;
                    
                    if (metaData.ValidateArguments(arguments))
                        return item;
                }
            }
            return null;
        }

        private readonly IDictionary<MethodHash, IList<IMethod>> methods;

    }
}
