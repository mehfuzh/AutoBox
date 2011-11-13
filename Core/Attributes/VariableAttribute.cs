using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Attributes
{
    /// <summary>
    /// Defines variable type argument.
    /// </summary>
    [AttributeUsage( AttributeTargets.Method)]
    internal sealed class VariableAttribute : System.Attribute
    {
    }
}
