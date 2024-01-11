namespace Zamat.Common.Multitenancy;

internal class NullTenantStore : ITenantStore
{
    public Task<Tenant?> GetTenantAsync(string tenantIdentifier)
    {
        return Task.FromResult<Tenant?>(new Tenant(tenantIdentifier));
    }
}
