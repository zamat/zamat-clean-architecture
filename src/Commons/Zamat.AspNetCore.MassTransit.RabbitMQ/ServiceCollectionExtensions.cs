using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.AspNetCore.MassTransit.RabbitMQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus, Action<IRabbitMqBusFactoryConfigurator> configureRabbitMQ)
    {
        services.AddMassTransit(c =>
        {
            configureBus(c);
            c.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(options.Prefix, false));
            c.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host);
                cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(options.Prefix, false));

                configureRabbitMQ(cfg);
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus)
    {
        services.ConfigureMassTransit(options, configureBus, configureRabbitMQ => { });

        return services;
    }

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, RabbitMQOptions options)
    {
        services.ConfigureMassTransit(options, configureBus => { }, configureRabbitMQ => { });

        return services;
    }

    public static IServiceCollection ConfigureMassTransitWithOutbox<TContext>(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus, Action<IRabbitMqBusFactoryConfigurator> configureRabbitMQ) where TContext : DbContext
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

    public static IServiceCollection ConfigureMassTransitWithOutbox<TContext>(this IServiceCollection services, RabbitMQOptions options, Action<IBusRegistrationConfigurator> configureBus) where TContext : DbContext
    {
        services.ConfigureMassTransitWithOutbox<TContext>(options, configureBus, configureRabbitMQ => { });

        return services;
    }

    public static IServiceCollection ConfigureMassTransitWithOutbox<TContext>(this IServiceCollection services, RabbitMQOptions options) where TContext : DbContext
    {
        services.ConfigureMassTransitWithOutbox<TContext>(options, configureBus => { }, configureRabbitMQ => { });

        return services;
    }
}
