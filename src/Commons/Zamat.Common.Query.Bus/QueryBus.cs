namespace Zamat.Common.Query.Bus;

class QueryBus(IQueryBusRegistry queryBusRegistry) : IQueryBus
{
    private readonly IQueryBusRegistry _queryBusRegistry = queryBusRegistry;

    public async Task<QueryResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var genericType = typeof(QueryWrapper<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var queryWrapper = (AbstractQueryWrapper<TResult>)Activator.CreateInstance(genericType)!;

        return await queryWrapper.HandleAsync(query, _queryBusRegistry, cancellationToken);
    }
}
