using MassTransit;
using MassTransit.Transport.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Audit.Consumer;
using Zamat.Sample.Services.Audit.Consumer.EventHandlers;
using Zamat.Sample.Services.Users.Core.Events;

namespace Zamat.Sample.Services.Audit.Consumer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<IEventHandler<UserCreated>, UserCreatedEventHandler>();

        return services;
    }

    public static IServiceCollection ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> configureBus)
    {
        var rabbitConnectionString = configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string for rabbitMQ not set.");

        var opt = new RabbitMQOptions()
        {
            Host = rabbitConnectionString,
            Prefix = "audit-consumer-svc"
        };

        services.ConfigureMassTransit(opt, configureBus, _ => { });

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConnectionString = configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string for rabbitMQ not set.");

        services.AddHealthChecks()
            .AddRabbitMQ(rabbitConnectionString: rabbitConnectionString);

        return services;
    }
}