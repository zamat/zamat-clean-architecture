using Microsoft.Extensions.DependencyInjection;

namespace AUMS.Common.Cache;

/// <summary>
/// Extensions methods for <see cref="IServiceCollection"/> registering the <see cref="ILocalCache"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extension method for <see cref="IServiceCollection"/> to add local cache services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddLocalCache(this IServiceCollection services)
    {
        return services.AddSingleton<ILocalCache, LocalCache>();
    }
}
