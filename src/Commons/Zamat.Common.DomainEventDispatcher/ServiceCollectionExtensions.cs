using Microsoft.Extensions.DependencyInjection;
using System;
using Zamat.BuildingBlocks.Domain;

namespace Zamat.Common.DomainEventDispatcher;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services, Action<IDomainEventDispatcherBuilder> setupAction)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        var builder = new DomainEventDispatcherBuilder(services);

        setupAction(builder);
        return services;
    }
}
