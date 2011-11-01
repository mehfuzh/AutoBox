using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Cache store entry provider.
    /// </summary>
    internal interface ICacheProvider
    {
        /// <summary>
        /// Gets the cached object for the specific key.
        /// </summary>
        object GetObject(string key);
       
        /// <summary>
        /// Sets the object to cache store for the specific key and sets the expiry date.
        /// </summary>
        void SetObject(string key, object value, DateTime expiry);
    }
}
