using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Query;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<QueryResult<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken = default!);
}
