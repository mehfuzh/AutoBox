using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoBox.Ast;

namespace AutoBox
{
    /// <summary>
    /// Wraps a specific argument.
    /// </summary>
    public class Argument
    {
        internal Argument(Expression expression)
        {
            this.expression = expression;
        }


        /// <summary>
        /// Gets the raw value from expression.
        /// </summary>
        internal object RawValue
        {
            get
            {
                VisitExpression();
                return visitor.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating if the argument can contain dynamic value.
        /// </summary>
        internal bool IsVariable
        {
            get
            {
                VisitExpression();
                return visitor.IsVariable;
            }
        }

        /// <summary>
        /// Validates the specific argument for equality.
        /// </summary>
        public bool Validate(object arg)
        {
            VisitExpression();

            if (visitor.IsVariable)
                return true;

            if (visitor.Type.IsPrimitive || visitor.Type.IsEnum)
                return visitor.Value.Equals(arg);
            if (typeof(string).IsAssignableFrom(visitor.Type))
                return visitor.Value == arg;
            
            return Object.ReferenceEquals(arg, visitor.Value);
        }

        private void VisitExpression()
        {
            if (visitor == null)
            {
                visitor = new ArgumentVisitor();
                visitor.Visit(expression);
            }
        }

        private bool isVariable;
        private string rawValue;
        private ArgumentVisitor visitor;
        private readonly Expression expression;
    }
}
