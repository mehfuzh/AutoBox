using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Defines various configuraiton settings.
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    public interface IConfigurationItem<T>
    {
        /// <summary>
        /// Specifies the cache duration.
        /// </summary>
        IConfiguration Caches(TimeSpan cacheDuration);

        /// <summary>
        /// Invalides the cache for the specific call.
        /// </summary>
        IConfiguration Invalidates(System.Linq.Expressions.Expression<Func<T, object>> expression);
    }
}
