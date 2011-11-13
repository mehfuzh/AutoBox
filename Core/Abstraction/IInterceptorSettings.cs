using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    internal interface IInterceptorSettings
    {
        /// <summary>
        /// Creates the proxy for the target type.
        /// </summary>
        object Create(Type targetType);
    }
}
