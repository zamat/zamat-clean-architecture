using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Query.Bus;

public interface IQueryBus
{
    Task<QueryResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}
