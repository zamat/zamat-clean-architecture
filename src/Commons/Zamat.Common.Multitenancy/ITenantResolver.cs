namespace AUMS.Common.Multitenancy;

public interface ITenantResolver
{
    Tenant Resolve();
}
