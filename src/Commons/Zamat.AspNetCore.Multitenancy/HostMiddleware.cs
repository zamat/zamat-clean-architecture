using System.Threading.Tasks;
using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Multitenancy;

internal class HostMiddleware
{
    private readonly RequestDelegate _next;

    public HostMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantStore tenantStore)
    {
        if (context.Items.ContainsKey(Constants.TenantKey))
        {
            await _next(context);
            return;
        }

        string host = context.Request.Host.Host;

        var tenant = await tenantStore.GetTenantByRealmAsync(host);
        if (tenant is not null)
        {
            context.Items.Add(Constants.TenantKey, tenant);
        }

        await _next(context);
    }
}
