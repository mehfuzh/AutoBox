using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoBox.Resolver.Abstraction;

namespace AutoBox.Resolver
{
    internal class AssemblyResolver : IResolver
    {
        /// <summary>
        /// Initializes the insance of the <see cref="AssemblyResolver"/> class.
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyResolver(Assembly assembly)
        {
            this.assembly = assembly;
        }

        /// <summary>
        /// Gets the target assembly to resolve type from.
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                Validate();
                return assembly;
            }
        }

        private void Validate()
        {
            if (assembly == this.GetType().Assembly)
            {
                throw new ArgumentException(Messages.CantResolveAutoConfigItself);
            }
        }

        private readonly Assembly assembly;
        
    }
}
