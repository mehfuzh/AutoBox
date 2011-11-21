using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Defines cache configuration.
    /// </summary>
    public interface ICacheConfiguration<T> : IConfigurationItem<T>
    {
        /// <summary>
        /// Specifies to cache based on variable arguments.
        /// </summary>
        IConfiguration VaryByArgs();
    }
}
