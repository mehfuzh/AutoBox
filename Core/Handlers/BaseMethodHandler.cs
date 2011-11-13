using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using System.Collections;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Handlers
{
    /// <summary>
    /// Original method handler.
    /// </summary>
    public class BaseMethodHandler : IHandler
    {
        /// <summary>
        /// Invokes the method defined in the current container using specific settings.
        /// </summary>
        public object Invoke(object target, IMethodInvocation invocation)
        {
            return invocation.Method.Invoke(target, invocation.Arguments);
        }
    }
}
