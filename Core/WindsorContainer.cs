using System;
using System.Linq;
using AutoBox.Abstraction;
using AutoBox.Attributes;
using AutoBox.Interceptors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AutoBox
{
    /// <summary>
    /// Windsor container.
    /// </summary>
    public class WindsorContainer : IContainer
    {
        public WindsorContainer()
        {
            container = new Castle.Windsor.WindsorContainer();
        }
        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        public void Register(Type @interface, Type targetType)
        {
            if (targetType.GetCustomAttributes(typeof(NoInterceptAttribute), false).Length == 0)
            {
                var targetProxiedInterceptor = typeof(ProxiedMethodInterceptor<>).MakeGenericType(targetType);

                if (!container.Kernel.HasComponent(targetProxiedInterceptor))
                    container.Register(Component.For(targetProxiedInterceptor));
               
                container.Register(Component.For(@interface).ImplementedBy(targetType).Interceptors(targetProxiedInterceptor));
               
                return;
            }
            container.Register(Component.For(@interface).ImplementedBy(targetType));
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType)
        {
            if (container.Kernel.HasComponent(targetType))
            {
                return container.Resolve(targetType);
            }
            return null;
        }

        private readonly IWindsorContainer container;
    }
}
