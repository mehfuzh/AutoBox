using System;
using System.Collections.Generic;
using System.Linq;
using AutoBox.Attributes;

namespace AutoBox
{
    /// <summary>
    /// Defines arguments behaviors.
    /// </summary>
    public static class Arg
    {
        /// <summary>
        /// Specifies that the argument can take variable input values.
        /// </summary>
        /// <typeparam name="T">Type of the argument</typeparam>
        [Variable]
        public static T Varies<T>()
        {
            return default(T);
        }
    }
}
