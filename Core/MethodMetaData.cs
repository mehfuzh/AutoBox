using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using AutoBox.Ast;

namespace AutoBox
{
    internal class MethodMetaData : Abstraction.IMethod
    {
        /// <summary>
        /// Initializes the new instance of <see cref="MethodMetaData"/> class.
        /// </summary>
        public MethodMetaData(MethodInfo methodInfo, Argument[] arguments) : this(new MethodHash(methodInfo), arguments)
        {
            // intentionally left blank.
        }

        /// <summary>
        /// Initializes the new instance of <see cref="MethodMetaData"/> class.
        /// </summary>
        public MethodMetaData(MethodHash methodHash, Argument[] arguments)
        {
            this.methodHash = methodHash;
            this.arguments = arguments;
        }

        public string Key
        {
            get
            {
                return string.Format("{0}{1}", methodHash.GetHashCode(), GetArgumentHashCode("+"));
            }
        }

        private string GetArgumentHashCode(string separator)
        {
            var builder = new StringBuilder();

            foreach (var argument in arguments)
            {
                builder.Append(separator);

                if (argument.IsVariable)
                    builder.Append(argument.IsVariable);
                else if (argument.RawValue == null)
                    builder.Append("null");
                else
                    builder.Append(argument.RawValue);
            }

            return builder.ToString();
        }

        public bool ValidateArguments(object[] arguments)
        {
            if (arguments.Length == this.arguments.Length)
            {
                bool result = true;
                
                for (int index = 0; index < this.arguments.Length; index++)
                    result &= this.arguments[index].Validate(arguments[index]);
       
                return result;
            }
            return false;
        }

        private readonly Argument[] arguments;
        private readonly MethodHash methodHash;
    }
}
