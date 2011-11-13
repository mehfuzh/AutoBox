using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    internal interface IHandler
    {
        /// <summary>
        /// Invokes the method defined in the current container using specific settings.
        /// </summary>
        object Invoke(object target, IMethodInvocation invocation);
    }
}
