using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

 
namespace AutoBox.Ast
{
    internal class MethodVisitor : Abstraction.ExpressionVisitor
    {
        /// <summary>
        /// Gets the method associated with the current expresssion.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return methodInfo;
            }
        }

        /// <summary>
        /// Gets the array of arguments.
        /// </summary>
        public Argument[] Arguments
        {
            get
            {
                return arguments;
            }
        }

        public override Expression VisitMethodCall(MethodCallExpression expression)
        {
            methodInfo = expression.Method;

            arguments = new Argument[expression.Arguments.Count];

            for (int index = 0; index < arguments.Length; index++)
            {
                arguments[index] = new Argument(expression.Arguments[index]);
            }

            return expression;
        }

        MethodInfo methodInfo;
        private Argument[] arguments;
    }
}
