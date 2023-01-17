using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MassTransit.Transport.RabbitMQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit<TContext>(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus, Action<IRabbitMqBusFactoryConfigurator> configureRabbitMQ) where TContext : DbContext
    {
        services.ConfigureMassTransit(options, c =>
        {
            configureBus(c);
            c.AddEntityFrameworkOutbox<TContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
        }, configureRabbitMQ);

        return services;
    }

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus, Action<IRabbitMqBusFactoryConfigurator> configureRabbitMQ)
    {
        services.AddMassTransit(c =>
        {
            configureBus(c);
            c.SetKebabCaseEndpointNameFormatter();
            c.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host);

                cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(options.Prefix, false));

                configureRabbitMQ(cfg);
            });
        });

        return services;
    }

}
