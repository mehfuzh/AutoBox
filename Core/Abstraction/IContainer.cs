using System;
using System.Linq;

namespace AutoBox.Abstraction
{
    /// <summary>
    /// Defines member for register and resolve type.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Registers an interface to its corresponding type.
        /// </summary>
        void Register(Type @interface, Type targetType);

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        object Resolve(Type targetType);
    }
}
