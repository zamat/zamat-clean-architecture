using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zamat.Common.Query.Bus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQueryBus(this IServiceCollection services, Action<IQueryBusBuilder> setupAction)
    {
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IQueryBusRegistry, QueryBusRegistry>();

        var builder = new QueryBusBuilder(services);

        setupAction(builder);

        return services;
    }
}
