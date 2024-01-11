﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Command.Bus;

internal class CommandBusBuilder(IServiceCollection serviceCollection) : ICommandBusBuilder
{
    private readonly IServiceCollection _serviceCollection = serviceCollection;

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
