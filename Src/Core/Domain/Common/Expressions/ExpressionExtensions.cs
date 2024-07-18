using System.Linq.Expressions;

namespace Domain.Common.Expressions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> srcExpression, Expression<Func<T, bool>> expressionToAdd)
    {
        var parameterExpression = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(srcExpression.Parameters[0], parameterExpression);
        var left = leftVisitor.Visit(srcExpression.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expressionToAdd.Parameters[0], parameterExpression);
        var right = rightVisitor.Visit(expressionToAdd.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameterExpression);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldExpression;
        private readonly Expression _newExpression;

        public ReplaceExpressionVisitor(Expression oldExpression, Expression newExpression)
        {
            _oldExpression = oldExpression;
            _newExpression = newExpression;
        }

        public override Expression? Visit(Expression? node)
        {
            if (node == _oldExpression)
                return _newExpression;

            return base.Visit(node);
        }
    }
}