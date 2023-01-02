using Zamat.Common.FilterQuery;
using System.Threading.Tasks;

namespace Zamat.Common.Paginator;

public interface IPaginator<TSource>
{
    Task<PaginableResult<TSource>> PaginateAsync(FilterQuery<TSource> paginableQuery, int page, int limit);
}
