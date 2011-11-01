using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using AutoBox.Containers;

namespace AutoBox.Locator
{
    public class AutoBoxServiceLocator : ServiceLocatorImplBase
    {
        public AutoBoxServiceLocator(TypeContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the container associated with this locator.
        /// </summary>
        public TypeContainer AutBox
        {
            get
            {
                return container;
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            throw new AutoConfigException("Iteration of all instances is not supported by AutoConfig");
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType);
        }

        private readonly TypeContainer container;
    }
}
