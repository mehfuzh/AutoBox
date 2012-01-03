using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AutoBox.Attributes;

namespace AutoBox.Ast
{
    internal class ArgumentVisitor : Abstraction.ExpressionVisitor
    {
        /// <summary>
        /// Gets extracted value for the argument.
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Gets the type of the current argument.
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets a value indicating that the argument is a varaible type.
        /// </summary>
        public bool IsVariable
        {
            get
            {
                return isVariable;
            }
        }

        public override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.GetCustomAttributes(typeof(VariableAttribute), false).Length > 0)
            {
                isVariable = true;
            }

            return expression;
        }

        public override Expression VisitMemberAccess(MemberExpression expression)
        {
            this.type = expression.Type;
            this.value = Expression.Lambda(expression).Compile().DynamicInvoke();
     
            return expression;
        }

        public override Expression VisitConstant(ConstantExpression expression)
        {
            this.type = expression.Type;
            this.value = expression.Value;
      
            return expression;
        }

        public override Expression VisitNew(NewExpression expression)
        {
            this.type = expression.Type;
            this.value = Expression.Lambda(expression).Compile().DynamicInvoke();

            return expression;
        }

        private bool isVariable;
        private object value;
        private Type type;
    }
}
