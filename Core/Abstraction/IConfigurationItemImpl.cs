using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoBox.Abstraction
{
    internal interface IConfigurationItemImpl
    {
        /// <summary>
        /// Gets the cache duration defined for the calling method.
        /// </summary>
        TimeSpan CacheDuration { get; }

        /// <summary>
        /// Gets or sets a value indicating the calling method is invalidated.
        /// </summary>
        bool IsInvalidated { get; set; }

        /// <summary>
        /// Gets the value indicating that argument validation should be skipped.
        /// </summary>
        bool IgnoreArgumentValidation { get; }

        /// <summary>
        /// Gets the resolved method.
        /// </summary>
        IMethod Method { get; }

        /// <summary>   
        /// Invalidates all the methods for the underlying call.
        /// </summary>
        void InvalidateDependencies();
    }
}
