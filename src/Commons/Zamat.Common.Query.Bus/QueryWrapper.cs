namespace Zamat.Common.Query.Bus;

internal class QueryWrapper<TQuery, TResult> : AbstractQueryWrapper<TResult> where TQuery : IQuery<TResult>
{
    public override async Task<QueryResult<TResult>> HandleAsync(IQuery<TResult> query, IQueryBusRegistry queryRegistry, CancellationToken cancellationToken)
    {
        var handler = queryRegistry.GetQueryHandler<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync((TQuery)query, cancellationToken);
    }
}
