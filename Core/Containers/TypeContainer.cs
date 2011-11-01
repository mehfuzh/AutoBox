using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoBox.Abstraction;
using AutoBox.Interceptors;

namespace AutoBox.Containers
{
    public class TypeContainer
    {
        public TypeContainer(Assembly assembly)
        {
            this.assembly = assembly;
            this.id = Guid.NewGuid().ToString();
            mappings = new Dictionary<Type, Type>();
            instances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Gets the instances associated with the current container.
        /// </summary>
        public IDictionary<Type, object> Instances
        {
            get
            {
                return instances;
            }
        }

        /// <summary>
        /// Gets the unique id of the container.
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }

        public void Register(Type @interface, Type targetType)
        {
            mappings.Add(@interface, targetType);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type targetType)
        {
            RegisterInterfaceWhenNecessary(targetType);

            if (mappings.ContainsKey(targetType))
            {
                object instance = null;

                if (!instances.ContainsKey(targetType))
                {
                    if (!typeof(ISpecific).IsAssignableFrom(mappings[targetType]))
                        instance = new ProxyGenerator(new MethodInterceptor(mappings[targetType])).Create(targetType);
                    else
                        instance = Activator.CreateInstance(mappings[targetType]);

                    instances.Add(targetType, instance);
                }
                return instances[targetType];
            }
            object result = ResolveDepencies(targetType);

            if (result == null)
                throw new ArgumentException(string.Format(Messages.NoSuitableCtorToResolve, targetType.Name));
            
            return result;
        }

        private void RegisterInterfaceWhenNecessary(Type interfaceType)
        {
            if (interfaceType.IsInterface && !mappings.ContainsKey(interfaceType))
            {
                string targetName = interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);

                IEnumerable<Type> resolvedTypes = assembly.GetTypes().Where(x => x.Name == targetName);

                foreach (var resolvedType in resolvedTypes)
                {
                    if (resolvedType != null && interfaceType.IsAssignableFrom(resolvedType))
                    {
                        Register(interfaceType, resolvedType);
                        break;
                    }
                }
            }
        }

        private object ResolveDepencies(Type targetType)
        {
            foreach (var constructor in targetType.GetConstructors())
            {
                var parameters = constructor.GetParameters();

                object[] args = new object[parameters.Length];

                for (int index = 0; index < parameters.Length; index++)
                {
                    var value = Resolve(parameters[index].ParameterType);

                    if (value == null)
                        continue;

                    args[index] = value;
                }
                return constructor.Invoke(args);
            }
            return null;
        }

        private readonly IDictionary<Type, Type> mappings;
        private readonly IDictionary<Type, object> instances;
        private readonly Assembly assembly;
        private readonly string id;

    }
}
