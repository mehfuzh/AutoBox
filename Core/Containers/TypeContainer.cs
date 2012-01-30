using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoBox.Abstraction;

namespace AutoBox.Containers
{
    /// <summary>
    /// Register and maps the target type.
    /// </summary>
    public class TypeContainer : IContainer
    {
        /// <summary>
        /// Initialize the instance of <see cref="TypeContainer"/> class.
        /// </summary>
        public TypeContainer(Assembly assembly, IContainer container)
        {
            this.assembly = assembly;
            this.container = container;
        }

        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        public void Register(Type @interface, Type targetType)
        {
            container.Register(@interface, targetType);
        }

        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType)
        {
            RegisterService(targetType);
           
            var instance = container.Resolve(targetType);

            if (instance != null)
                return instance;
            
            object result = ResolveDepencies(targetType);

            if (result == null)
                throw new ArgumentException(string.Format(Messages.NoSuitableCtorToResolve, targetType.Name));
            
            return result;
        }

        /// <summary>
        /// Resolves all registered instances for a specific service type.
        /// </summary>
        public IList<object> ResolveAll(Type serviceType)
        {
            RegisterService(serviceType);
            return container.ResolveAll(serviceType);
        }

        private void RegisterService(Type serviceType)
        {
            if (serviceType.IsInterface && container.Resolve(serviceType) == null)
            {
                IEnumerable<Type> resolvedTypes = assembly.GetTypes().Where(x => x.GetInterfaces().Any(t => t == serviceType));
                
                if (resolvedTypes.Count() == 0)
                {
                    throw new AutoBoxException(string.Format(Messages.FailedToResolveCorrespondingType, serviceType.Name));
                }

                foreach (var resolvedType in resolvedTypes)
                {
                    if (resolvedType != null && serviceType.IsAssignableFrom(resolvedType))
                    {
                        Register(serviceType, resolvedType);
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

        private readonly Assembly assembly;
        private readonly IContainer container;
    }
}
