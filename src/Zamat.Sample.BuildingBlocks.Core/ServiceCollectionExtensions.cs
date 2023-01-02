using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Sample.BuildingBlocks.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreBuildingBlocks(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IUuidGenerator, UuidGenerator>();

        return services;
    }
}