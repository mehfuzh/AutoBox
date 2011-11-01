using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoBox.Resolver.Abstraction
{
    /// <summary>
    /// Defines assembly to resolve.
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Gets the target assembly to resolve type from.
        /// </summary>
        Assembly Assembly { get; }
    }
}
