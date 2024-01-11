namespace Zamat.Common.Multitenancy;

public interface ITenantStore
{
    Task<Tenant?> GetTenantAsync(string tenantIdentifier);
}
