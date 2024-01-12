using Microsoft.Extensions.DependencyInjection;

namespace AUMS.Common.Multitenancy;

public static class ServiceCollectionExtensions
{
    public static TenantBuilder AddMultitenancy(this IServiceCollection services)
    {
        services.AddScoped<TenantContextAccessor>();

        return new TenantBuilder(services);
    }
}
