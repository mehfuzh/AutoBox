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
        [Variable]
        public static T Varies<T>()
        {
            return default(T);
        }
    }
}
