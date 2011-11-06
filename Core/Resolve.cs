using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoBox.Abstraction;
using System.Reflection;
using AutoBox.Resolver.Abstraction;
using AutoBox.Resolver;

namespace AutoBox
{
    /// <summary>
    /// Defines methods to resolve assembly under which the container will initialize.
    /// </summary>
    public static class Resolve
    {
        /// <summary>
        /// Resolve assembly from current.
        /// </summary>
        public static IResolver FromCurrentAssembly
        {
            get
            {
                return new AssemblyResolver(Assembly.GetCallingAssembly());
            }
        }

        /// <summary>
        /// Resolves from the target assembly.
        /// </summary>
        /// <param name="assembly">Target assenbly</param>
        public static IResolver From(Assembly assembly)
        {
            return new AssemblyResolver(assembly);
        }
    }
}
