using AUMS.Common.Multitenancy;

namespace AUMS.AspNetCore.Multitenancy;

public static class TenantBuilderExtensions
{
    public static TenantBuilder AddHttpContextResolver(this TenantBuilder tenantBuilder)
    {
        tenantBuilder.AddResolver<HttpContextTenantResolver>();

        return tenantBuilder;
    }
}
