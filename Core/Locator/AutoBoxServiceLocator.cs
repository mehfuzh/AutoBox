using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Abstraction;

namespace AutoBox.Locator
{
    /// <summary>
    /// Service locator
    /// </summary>
    public class AutoBoxServiceLocator : ServiceLocatorImplBase
    {
        internal AutoBoxServiceLocator(IContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the container associated with this locator.
        /// </summary>
        internal IContainer Container
        {
            get
            {
                return container;
            }
        }

        /// <summary>
        /// Gets all the instances for a target type.
        /// </summary>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            throw new AutoBoxException("Iteration of all instances is not supported");
        }

        /// <summary>
        /// Gets the specific instance for target type.
        /// </summary>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType != null)
            {
                return container.Resolve(serviceType);
            }
            return null;
        }

        /// <summary>
        /// Formats any <see cref="AutoBoxException"/> exceptions.
        /// </summary>
        protected override string FormatActivationExceptionMessage(Exception actualException, Type serviceType, string key)
        {
            if (actualException is AutoBoxException)
            {
                return actualException.Message;
            }
            return base.FormatActivationExceptionMessage(actualException, serviceType, key);
        }

        private readonly IContainer container;
    }
}
