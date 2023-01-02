using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MassTransit.Transport.InMemory;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services, Action<IBusRegistrationConfigurator>? configure = null) where TContext : DbContext
    {
        services.AddMassTransit(c =>
        {
            if (configure is not null) configure(c);

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
}
