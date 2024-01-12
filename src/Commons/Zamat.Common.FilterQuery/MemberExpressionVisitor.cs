using System.Linq.Expressions;
using System.Reflection;

namespace Zamat.Common.FilterQuery;

internal class MemberExpressionVisitor : ExpressionVisitor
{
    protected override Expression VisitMember(MemberExpression memberExpression)
    {
        var expression = Visit(memberExpression.Expression);
        if (expression is ConstantExpression constantExpression)
        {
            var container = constantExpression.Value;

            var member = memberExpression.Member;

            if (member is FieldInfo fieldInfo)
            {
                object value = fieldInfo.GetValue(container)!;
                return Expression.Constant(value);
            }

            if (member is PropertyInfo propertyInfo)
            {
                object value = propertyInfo.GetValue(container)!;
                return Expression.Constant(value);
            }
        }

        return base.VisitMember(memberExpression);
    }
}
