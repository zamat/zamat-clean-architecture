using Zamat.Common.FilterQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zamat.Common.Paginator;

public interface IPaginableSource<TSource>
{
    Task<IEnumerable<TSource>> GetAsync(FilterQuery<TSource> query, int page = 1, int limit = 10);
    Task<int> CountAsync(FilterQuery<TSource> query);
}
