using Microsoft.Extensions.DependencyInjection;
using Zamat.BuildingBlocks.Core;

namespace Zamat.Clean.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreCommon(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();

        return services;
    }
}