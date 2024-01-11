using System.Linq.Expressions;

namespace Zamat.Common.FilterQuery;

class OrderBy<TSource>(bool descending, Expression<Func<TSource, object>> expression)
{
    public bool Descending { get; } = descending;
    public Expression<Func<TSource, object>> Expression { get; } = expression;
}
