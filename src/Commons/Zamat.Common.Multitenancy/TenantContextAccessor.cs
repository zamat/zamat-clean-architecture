namespace Zamat.Common.Multitenancy;

public class TenantContextAccessor(ITenantResolver tenantResolver)
{
    private readonly ITenantResolver _tenantResolver = tenantResolver;

    public Tenant GetCurrentTenant()
    {
        return new Tenant(_tenantResolver.Resolve());
    }
}
