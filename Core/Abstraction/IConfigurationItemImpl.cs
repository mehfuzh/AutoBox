using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBox.Abstraction
{
    internal interface IConfigurationItemImpl
    {
        /// <summary>
        /// Invalidates all the methods for the underlying call.
        /// </summary>
        void InvalidateDependencies();

        /// <summary>
        /// Gets or sets a value indicating the calling method is invalidated.
        /// </summary>
        bool InValidated { get; set; }

        /// <summary>
        /// Gets the cache duration defined for the calling method.
        /// </summary>
        TimeSpan CacheDuration { get; }
    }
}
