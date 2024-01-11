using Microsoft.Extensions.DependencyInjection;
using Zamat.Common.Events.Bus.MassTransit;

namespace Zamat.Clean.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddMassTransitEventBus();

        return services;
    }
}