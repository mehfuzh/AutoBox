using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using System.Reflection;

namespace AutoBox
{
    /// <summary>
    /// Wraps the dynamic method invocation.
    /// </summary>
    public class MethodInvocation : IMethodInvocation
    {
        /// <summary>
        /// Initializes the new instance of the <see cref="MethodInvocation"/> class.
        /// </summary>
        /// <param name="target">Target instance</param>
        /// <param name="method">Method that wraps the invocation</param>
        /// <param name="arguments">Arguments pass during invocation</param>
        public MethodInvocation(object target, MethodInfo method, object[] arguments)
        {
            this.target = target;
            this.method = method;
            this.arguments = arguments;
        }
   
        /// <summary>
        /// Gets a value indicating whether to call the original implementation for the method.
        /// </summary>
        public bool InvokeOriginal
        {
            get
            {
                return invokeOriginal;
            }
        }
        
        /// <summary>
        /// Gets the return value.
        /// </summary>
        public object ReturnValue
        {
            get { return returnValue; }
        }

        /// <summary>
        /// Gets the target method.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return method;
            }
        }

        /// <summary>
        /// Gets the invoked arguments.
        /// </summary>
        public object[] Arguments
        {
            get
            {
                return arguments;
            }
        }

        /// <summary>
        /// Sets the specific return value.
        /// </summary>
        public void SetReturn(object returnValue)
        {
            this.returnValue = returnValue;
        }

        /// <summary>
        /// Marks to invoke the original mehtod invocation.
        /// </summary>
        public void Continue()
        {
            invokeOriginal = true;
        }

        private readonly object target;
        private readonly MethodInfo method;
        private object[] arguments;
        private bool invokeOriginal;
        private object returnValue;
    }
}
