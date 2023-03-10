using Microsoft.Extensions.DependencyInjection;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Content.Core.Services;

namespace Zamat.Sample.Services.Content.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();

        services.AddScoped<ArticleService>();

        return services;
    }
}
