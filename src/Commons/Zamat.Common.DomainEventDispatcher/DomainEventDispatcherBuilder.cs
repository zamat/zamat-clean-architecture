using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Zamat.BuildingBlocks.Domain;

namespace Zamat.Common.DomainEventDispatcher;

class DomainEventDispatcherBuilder : IDomainEventDispatcherBuilder
{
    private readonly IServiceCollection _serviceCollection;

    public DomainEventDispatcherBuilder(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public IDomainEventDispatcherBuilder AddDomainEventHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        {
            assembly.GetTypes().Where(
                type => type.IsClass &&
                !type.IsAbstract && type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))).ToList()
                .ForEach(handler =>
                {
                    _serviceCollection.Add(
                        new ServiceDescriptor(
                            handler.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)),
                            handler, serviceLifetime
                            ));
                });

            return this;
        }
    }
}
