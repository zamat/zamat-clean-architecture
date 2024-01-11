using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Query.Bus;

class QueryBusRegistry(IServiceProvider serviceProvider) : IQueryBusRegistry
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public THandler GetQueryHandler<THandler>()
    {
        var handler = _serviceProvider.GetService<THandler>();
        if (handler is null)
        {
            throw new QueryBusException($"Query {typeof(THandler).Name} handler not found");
        }
        return handler;
    }
}
