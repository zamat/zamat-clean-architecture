using System;
using System.Linq.Expressions;

namespace Zamat.Common.Paginator;

class OrderBy<TSource>
{
    public bool Descending { get; }
    public Expression<Func<TSource, object>> Expression { get; }

    public OrderBy(bool descending, Expression<Func<TSource, object>> expression)
    {
        Descending = descending;
        Expression = expression;
    }
}
