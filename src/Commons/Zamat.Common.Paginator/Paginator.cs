using Zamat.Common.FilterQuery;
using System.Threading.Tasks;

namespace Zamat.Common.Paginator;

class Paginator<TSource> : IPaginator<TSource>
{
    private readonly IPaginableSource<TSource> _source;

    public Paginator(IPaginableSource<TSource> source)
    {
        _source = source;
    }

    public async Task<PaginableResult<TSource>> PaginateAsync(FilterQuery<TSource> paginableQuery, int page, int limit)
    {
        var paginableData = await _source.GetAsync(paginableQuery, page, limit);
        var paginableResult = new PaginableResult<TSource>(
            page, limit, await _source.CountAsync(paginableQuery), paginableData);
        return paginableResult;
    }
}
