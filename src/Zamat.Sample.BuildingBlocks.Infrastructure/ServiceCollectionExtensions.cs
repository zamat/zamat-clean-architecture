using Microsoft.Extensions.DependencyInjection;
using Zamat.Common.Events.Bus.MassTransit;

namespace Zamat.Sample.BuildingBlocks.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuildingBlocks(this IServiceCollection services)
    {
        services.AddEventBus();

        return services;
    }
}