namespace AUMS.Common.Multitenancy;

public class TenantContextAccessor
{
    private readonly ITenantResolver _tenantResolver;

    public TenantContextAccessor(ITenantResolver tenantResolver)
    {
        _tenantResolver = tenantResolver;
    }

    public Tenant GetCurrentTenant()
    {
        return _tenantResolver.Resolve();
    }
}
