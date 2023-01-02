using Zamat.Common.Multitenancy;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Zamat.AspNetCore.Multitenancy;

class HostMiddleware
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

        var tenant = await tenantStore.GetTenantAsync(host);
        if (tenant is null)
        {
            throw new TenantNotFoundException($"Tenant for given host not found. (hostname: {host})");
        }

        context.Items.Add(Constants.TenantKey, tenant.Identifier);

        await _next(context);
    }
}
