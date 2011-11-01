using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using System.Collections;
using Microsoft.Practices.ServiceLocation;

namespace AutoBox.Handlers
{
    public class BaseMethodHandler : IHandler
    {
        public object Invoke(object target, IMethodInvocation invocation)
        {
            return invocation.Method.Invoke(target, invocation.Arguments);
        }
    }
}
