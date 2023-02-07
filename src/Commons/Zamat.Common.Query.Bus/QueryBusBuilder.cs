using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Query.Bus;

class QueryBusBuilder : IQueryBusBuilder
{
    private readonly IServiceCollection _serviceCollection;

    public QueryBusBuilder(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;

    }

    public IQueryBusBuilder AddQueryHandlers(Assembly assembly)
    {
        assembly.GetTypes().Where(
            type => type.IsClass &&
            !type.IsAbstract && type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))).ToList()
            .ForEach(handler =>
                _serviceCollection.Add(
                    new ServiceDescriptor(
                        handler.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)),
                        handler, ServiceLifetime.Scoped)
            ));

        return this;
    }
}
