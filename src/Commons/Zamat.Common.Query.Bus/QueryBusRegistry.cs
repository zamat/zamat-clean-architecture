using Zamat.Common.Query.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zamat.Common.Query.Bus;

class QueryBusRegistry : IQueryBusRegistry
{
    private readonly IServiceProvider _serviceProvider;

    public QueryBusRegistry(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

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
