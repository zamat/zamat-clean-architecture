using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Events.Bus.MassTransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddScoped<IEventBus, EventBus>();
        return services;
    }
}
