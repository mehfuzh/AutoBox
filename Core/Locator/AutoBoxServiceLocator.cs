using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Containers;

namespace AutoBox.Locator
{
    /// <summary>
    /// Service locator
    /// </summary>
    public class AutoBoxServiceLocator : ServiceLocatorImplBase
    {
        internal AutoBoxServiceLocator(TypeContainer typeContainer)
        {
            this.typeContainer = typeContainer;
        }

        /// <summary>
        /// Gets the container associated with this locator.
        /// </summary>
        internal TypeContainer TypeContainer
        {
            get
            {
                return typeContainer;
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
                return typeContainer.Resolve(serviceType);
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

        private readonly TypeContainer typeContainer;
    }
}
