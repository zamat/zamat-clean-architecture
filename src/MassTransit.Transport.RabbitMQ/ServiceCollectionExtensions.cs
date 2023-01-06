using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MassTransit.Transport.RabbitMQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services, string host, Action<IBusRegistrationConfigurator> configureBus, Action<IRabbitMqBusFactoryConfigurator> configureRabbitMQ) where TContext : DbContext
    {
        services.AddMassTransit(c =>
        {
            configureBus(c);
            
            c.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
            
            c.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host);

                cfg.ConfigureEndpoints(context);

                configureRabbitMQ(cfg);
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services, string host) where TContext : DbContext
    {
        services.ConfigureMassTransit<TContext>(host, (_) => { }, (_) => { });

        return services;
    }
}
