using System.Linq.Expressions;

namespace AutoBox.Ast.Abstraction
{
    /// <summary>
    /// Expression visitor 
    /// </summary>
    internal abstract class ExpressionVisitor
    {
        /// <summary>
        /// Visits expression and delegates call to different to branch.
        /// </summary>
        /// <param name="expression">Target expression</param>
        /// <returns>Original expression</returns>
        public virtual Expression Visit(Expression expression)
        {
            if (expression == null)
            {
                return null;
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return this.VisitLamda((LambdaExpression)expression);
                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return this.VisitUnary((UnaryExpression)expression);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return this.VisitBinary((BinaryExpression)expression);
                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression)expression);
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)expression);
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)expression);
                case ExpressionType.Parameter:
                    return this.VisiParameter((ParameterExpression)expression);
                case ExpressionType.New:
                    return this.VisitNew((NewExpression) expression);
                case ExpressionType.NewArrayInit:
                    return this.VisitNewArrayInit((NewArrayExpression)expression);
            }
            return expression;
        }

        /// <summary>
        /// Visits parameter expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisiParameter(ParameterExpression expression)
        {
            return expression;
        }

        public virtual Expression VisitNew(NewExpression expression)
        {
            return expression;   
        }

        /// <summary>
        /// Visits the constance expression. To be implemented by user.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitConstant(ConstantExpression expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits the memeber access expression. To be implemented by user.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitMemberAccess(MemberExpression expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits the method call expression. To be implemented by user.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitMethodCall(MethodCallExpression expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits the binary expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitBinary(BinaryExpression expression)
        {
            this.Visit(expression.Left);
            this.Visit(expression.Right);
            return expression;
        }

        /// <summary>
        /// Visits the unary expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitUnary(UnaryExpression expression)
        {
            this.Visit(expression.Operand);
            return expression;
        }

        /// <summary>
        /// Visits the lamda expression.
        /// </summary>
        /// <param name="lambdaExpression"></param>
        /// <returns></returns>
        public virtual Expression VisitLamda(LambdaExpression lambdaExpression)
        {
            this.Visit(lambdaExpression.Body);
            return lambdaExpression;
        }

        /// <summary>
        /// Visits the new array init expression. To be implemented by user.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual Expression VisitNewArrayInit(NewArrayExpression expression)
        {
            return expression;
        }
    }
}

