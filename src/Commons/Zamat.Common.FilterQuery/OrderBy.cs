using System.Linq.Expressions;

namespace Zamat.Common.FilterQuery;

internal class OrderBy<TSource>
{
    public OrderBy(bool descending, Expression<Func<TSource, object>> expression)
    {
        Descending = descending;
        Expression = expression;
    }

    public bool Descending { get; }

    public Expression<Func<TSource, object>> Expression { get; }
}
