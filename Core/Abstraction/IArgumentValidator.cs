using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Validates method setup for expected arguments.
    /// </summary>
    public interface IArgumentValidator
    {
        /// <summary>
        /// Validates the container method for invoking arguments.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        bool ValidateArguments(object[] arguments);
    }
}
