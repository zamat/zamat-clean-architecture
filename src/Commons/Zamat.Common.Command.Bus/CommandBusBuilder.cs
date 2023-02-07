using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Command.Bus;

class CommandBusBuilder : ICommandBusBuilder
{
    private readonly IServiceCollection _serviceCollection;

    public CommandBusBuilder(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public ICommandBusBuilder AddCommandHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        assembly.GetTypes().Where(
            type => type.IsClass &&
            !type.IsAbstract && type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))).ToList()
            .ForEach(handler =>
            {
                _serviceCollection.Add(
                    new ServiceDescriptor(
                        handler.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)),
                        handler, serviceLifetime
                        ));
            });

        return this;
    }
}
