using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Multitenancy;

internal class HttpContextTenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTenantResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Tenant Resolve()
    {
        var context = _httpContextAccessor.HttpContext!;
        if (!context.Items.TryGetValue(Constants.TenantKey, out object? item))
        {
            throw new TenantNotFoundException("Tenant context is missing.");
        }

        if (item is null)
        {
            throw new TenantNotFoundException("Tenant context is missing.");
        }

        return (Tenant)item;
    }
}
