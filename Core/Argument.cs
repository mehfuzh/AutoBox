using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoBox.Ast;

namespace AutoBox
{
    public class Argument
    {
        public Argument(Expression expression)
        {
            this.id = Guid.NewGuid();
            this.expression = expression;
        }

        /// <summary>
        /// Gets the unique id for the current argument.
        /// </summary>
        public string Id
        {
            get
            {
                return id.ToString().Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Validates the specific argument for equality.
        /// </summary>
        public bool Validate(object arg)
        {
            var visitor = new ArgumentVisitor();

            visitor.Visit(expression);

            if (visitor.IsVariable)
                return true;

            if (visitor.Type.IsPrimitive || visitor.Type.IsEnum)
            {
                return visitor.Value.Equals(arg);
            }
            else if (typeof(string).IsAssignableFrom(visitor.Type))
            {
                return visitor.Value == arg;
            }

            return Object.ReferenceEquals(arg, visitor.Value);
        }

        private readonly Guid id;
        private readonly Expression expression;
    }
}
