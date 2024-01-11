using System.Linq.Expressions;

namespace Zamat.Common.FilterQuery;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> source, bool condition, Expression<Func<T, bool>> target)
        => condition ? Expression.Lambda<Func<T, bool>>(Expression.AndAlso(source.Body, Expression.Invoke(target, source.Parameters.Cast<Expression>())), source.Parameters) : source;
}
