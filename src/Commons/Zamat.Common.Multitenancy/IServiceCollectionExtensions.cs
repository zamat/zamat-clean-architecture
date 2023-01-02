using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Multitenancy;

public static class IServiceCollectionExtensions
{
    public static TenantBuilder AddMultitenancy(this IServiceCollection services)
    {
        services.AddScoped<ITenantStore, NullTenantStore>();
        services.AddScoped<TenantContextAccessor>();

        return new TenantBuilder(services);
    }
}
