using System;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.AspNetCore.MassTransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, Action<IBusRegistrationConfigurator> configureBus)
    {
        services.AddMassTransit(c =>
        {
            configureBus(c);
            c.SetKebabCaseEndpointNameFormatter();
            c.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureMassTransitWithOutbox<TContext>(this IServiceCollection services, Action<IBusRegistrationConfigurator> configureBus) where TContext : DbContext
    {
        services.ConfigureMassTransit(c =>
        {
            configureBus(c);
            c.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
        });
        return services;
    }
}
