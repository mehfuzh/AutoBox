using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoBox
{
    internal class MethodHash
    {
        /// <summary>
        /// Initializes the new instance of <see cref="MethodInfoKey"/> class.
        /// </summary>
        /// <param name="methodInfo">Target <see cref="MethodInfo"/> instance</param>
        internal MethodHash(MethodInfo methodInfo)
        {
            this.name = methodInfo.Name;
            this.arguments = methodInfo.GetParameters().Select(x => x.ParameterType).ToArray();
            this.genArguments = methodInfo.GetGenericArguments();
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (hashCode == 0)
            {
                hashCode = this.name.GetHashCode();

                foreach (Type argument in this.arguments)
                    hashCode += argument.GetHashCode();

                foreach (Type genArgument in this.genArguments)
                    hashCode += genArgument.GetHashCode();

            }
            return hashCode;
        }

        private int hashCode;
        private readonly string name;
        private readonly Type[] arguments;
        private readonly Type[] genArguments;
    }
}
