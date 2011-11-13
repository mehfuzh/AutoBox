using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Defines the resolved method.
    /// </summary>
    public interface IMethod
    {
        /// <summary>
        /// Gets the unique for the resolved method.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Validates the container method for invoking arguments.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        bool ValidateArguments(object[] arguments);
    }
}
