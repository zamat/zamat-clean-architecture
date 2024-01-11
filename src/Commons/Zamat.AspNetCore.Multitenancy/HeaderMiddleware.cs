using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zamat.Common.Multitenancy;

namespace Zamat.AspNetCore.Multitenancy;

class HeaderMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, ITenantStore tenantStore)
    {
        if (context.Items.ContainsKey(Constants.TenantKey))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(Constants.TenantHeader, out var value))
        {
            await _next(context);
            return;
        }

        var tenant = await tenantStore.GetTenantAsync(value.ToString());
        if (tenant is null)
        {
            throw new TenantNotFoundException($"Tenant for given header not found. (header: {value})");
        }

        context.Items.Add(Constants.TenantKey, tenant.Identifier);

        await _next(context);
    }
}

