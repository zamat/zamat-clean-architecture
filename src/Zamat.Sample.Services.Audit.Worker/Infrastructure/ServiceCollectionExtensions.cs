using MassTransit;
using Microsoft.EntityFrameworkCore;
using Zamat.AspNetCore.MassTransit.RabbitMQ;
using Zamat.Sample.Services.Audit.Worker.Infrastructure;

namespace Zamat.Sample.Services.Audit.Worker.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> configureBus)
    {
        services.ConfigureMessageBroker(configuration, configureBus);

        return services;
    }

    static IServiceCollection ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration, Action<IBusRegistrationConfigurator> configureBus)
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