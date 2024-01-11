namespace Zamat.Common.Query.Bus;

abstract class AbstractQueryWrapper<TResult>
{
    public abstract Task<QueryResult<TResult>> HandleAsync(IQuery<TResult> query, IQueryBusRegistry queryRegistry, CancellationToken cancellationToken);
}
