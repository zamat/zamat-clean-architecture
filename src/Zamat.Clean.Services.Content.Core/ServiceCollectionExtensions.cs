using Microsoft.Extensions.DependencyInjection;
using Zamat.Clean.Services.Content.Core.Services;
using Zamat.Clean.BuildingBlocks.Core;

namespace Zamat.Clean.Services.Content.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();

        services.AddScoped<ArticleService>();

        return services;
    }
}
