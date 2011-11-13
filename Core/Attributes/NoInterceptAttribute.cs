using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Attributes
{
    /// <summary>
    /// Specifies that the type should not be intercepted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class NoInterceptAttribute : System.Attribute
    {
    }
}
