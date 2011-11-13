using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Wraps a method invocaiton.
    /// </summary>
    public interface IMethodInvocation
    {
        /// <summary>
        /// Gets the target method.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Gets the invoked arguments.
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// Continue to original method.
        /// </summary>
        void Continue();

        /// <summary>
        /// Sets the the specific return value.
        /// </summary>
        void SetReturn(object returnValue);
    }
}
