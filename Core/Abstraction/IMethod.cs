﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Defines the resolved method.
    /// </summary>
    public interface IMethod : IArgumentValidator
    {
        /// <summary>
        /// Gets the unique key for the resolved method.
        /// </summary>
        string Key { get; }
    }
}
