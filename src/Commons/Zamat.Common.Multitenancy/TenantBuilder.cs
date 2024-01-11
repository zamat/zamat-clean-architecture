using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Multitenancy;

public class TenantBuilder(IServiceCollection services)
{
    private readonly IServiceCollection _services = services;

    public TenantBuilder AddResolver<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped) where T : class, ITenantResolver
    {
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolver), typeof(T), lifetime));
        return this;
    }

    public TenantBuilder AddTenantStore<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped) where T : class, ITenantStore
    {
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore), typeof(T), lifetime));
        return this;
    }
}
