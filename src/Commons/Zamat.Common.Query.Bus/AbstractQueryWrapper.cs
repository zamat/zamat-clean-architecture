namespace Zamat.Common.Query.Bus;

internal abstract class AbstractQueryWrapper<TResult>
{
    public abstract Task<QueryResult<TResult>> HandleAsync(IQuery<TResult> query, IQueryBusRegistry queryRegistry, CancellationToken cancellationToken);
}
