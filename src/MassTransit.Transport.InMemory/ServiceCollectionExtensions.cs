using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MassTransit.Transport.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services, Action<IBusRegistrationConfigurator> busConfig) where TContext : DbContext
    {
        services.AddMassTransit(c =>
        {
            busConfig(c);

            c.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });

            c.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.ConfigureMassTransit<TContext>((_) => { });

        return services;
    }
}
