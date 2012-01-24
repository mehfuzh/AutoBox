using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using AutoBox.Attributes;
using AutoBox.Interceptors;

namespace AutoBox
{
    public class StandardContainer : IContainer
    {
        /// <summary>
        /// Initialize the instance of <see cref="TypeContainer"/> class.
        /// </summary>
        public StandardContainer()
        {
            mappings = new Dictionary<Type, Type>();
            instances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        public void Register(Type @interface, Type targetType)
        {
            mappings.Add(@interface, targetType);
        }

        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        public object Resolve(Type targetType)
        {
            if (mappings.ContainsKey(targetType))
            {
                object instance = null;

                if (!instances.ContainsKey(targetType))
                {
                    if (mappings[targetType].GetCustomAttributes(typeof(NoInterceptAttribute), false).Length == 0)
                        instance = new ProxyGenerator(new MethodInterceptor(mappings[targetType])).Create(targetType);
                    else
                        instance = Activator.CreateInstance(mappings[targetType]);

                    instances.Add(targetType, instance);
                }
                return instances[targetType];
            }
            return null;
        }

        private readonly IDictionary<Type, Type> mappings;
        private readonly IDictionary<Type, object> instances;
    }
}
