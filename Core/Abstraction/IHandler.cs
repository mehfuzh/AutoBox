using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    internal interface IHandler
    {
        /// <summary>
        /// Invokes the method defined for the current config.
        /// </summary>
        object Invoke(object target, IMethodInvocation invocation);
    }
}
