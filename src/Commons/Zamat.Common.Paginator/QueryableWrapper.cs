using Zamat.Common.FilterQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zamat.Common.Paginator;

public class QueryableWrapper<TSource> : IPaginableSource<TSource>
{
    private IQueryable<TSource> _sources;

    public QueryableWrapper(IQueryable<TSource> sources)
    {
        _sources = sources;
    }

    public Task<int> CountAsync(FilterQuery<TSource> query)
    {
        _ = query ?? throw new ArgumentNullException(nameof(query), "FilterQuery cannot be empty");
        var count = _sources.Where(query.Expression).Count();
        return Task.FromResult(count);
    }

    public Task<IEnumerable<TSource>> GetAsync(FilterQuery<TSource> query, int page = 1, int limit = 10)
    {
        _ = query ?? throw new ArgumentNullException(nameof(query), "FilterQuery cannot be empty");
        var queryable = _sources.Where(query.Expression).Skip(page * limit).Take(limit);
        if (query.AsDescending)
            queryable = queryable.OrderByDescending(query.OrderBy);
        else
            queryable = queryable.OrderBy(query.OrderBy);
        return Task.FromResult(queryable.AsEnumerable());
    }
}
