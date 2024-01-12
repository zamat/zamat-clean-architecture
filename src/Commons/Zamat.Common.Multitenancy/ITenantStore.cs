using System.Threading.Tasks;

namespace AUMS.Common.Multitenancy;

public interface ITenantStore
{
    Task<Tenant?> GetTenantAsync(string tenantId);
    Task<Tenant?> GetTenantByRealmAsync(string realm);
}
