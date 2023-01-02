using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Zamat.Common.FilterQuery;

public class FilterQuery<TSource>
{
    private Expression<Func<TSource, bool>> _filterExpression = (source) => true;

    private readonly Dictionary<Enum, OrderBy<TSource>> _orderBy = new();

    public Enum Sort { get; }

    public FilterQuery(Enum sort)
    {
        Sort = sort;
    }

    public void Filter(bool condition, Expression<Func<TSource, bool>> expression)
    {
        _filterExpression = _filterExpression.And(condition, expression);
    }
    public void Filter(Expression<Func<TSource, bool>> expression)
    {
        _filterExpression = _filterExpression.And(1 == 1, expression);
    }
    public void SortMap(Enum sortStrategy, Expression<Func<TSource, object>> expression, bool isDescending)
    {
        _orderBy[sortStrategy] = new OrderBy<TSource>(isDescending, expression);
    }

    public Expression<Func<TSource, bool>> Expression => _filterExpression.Expand();

    public Expression<Func<TSource, object>> OrderBy
    {
        get
        {
            if (_orderBy.ContainsKey(Sort))
                return _orderBy[Sort].Expression.Expand();
            throw new InvalidOperationException("Missing sort strategy for given key");
        }
    }

    public bool AsDescending
    {
        get
        {
            if (_orderBy.ContainsKey(Sort))
                return _orderBy[Sort].Descending;
            throw new InvalidOperationException("Missing sort strategy for given key");
        }
    }

    public QueryParams QueryParams
    {
        get
        {
            MemberExpressionVisitor memberExpressionVisitor = new();
            BinaryExpressionVisitor binaryExpressionVisitor = new();
            var visited = memberExpressionVisitor.Visit(Expression);
            binaryExpressionVisitor.Visit(visited);
            var queryParams = binaryExpressionVisitor.QueryParams;
            return queryParams;
        }
    }
}
