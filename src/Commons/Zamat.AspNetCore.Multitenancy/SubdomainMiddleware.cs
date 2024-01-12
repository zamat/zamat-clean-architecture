using System;
using System.Threading.Tasks;
using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Multitenancy;

internal class SubdomainMiddleware
{
    private readonly RequestDelegate _next;

    public SubdomainMiddleware(RequestDelegate next)
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

        string subDomain = string.Empty;

        string host = context.Request.Host.Host;

        if (!string.IsNullOrWhiteSpace(host))
        {
            subDomain = host.Split('.')[0];
        }

        subDomain = subDomain.Trim().ToLower();

        var tenant = await tenantStore.GetTenantByRealmAsync(subDomain);
        if (tenant is not null)
        {
            context.Items.Add(Constants.TenantKey, tenant);
        }

        await _next(context);
    }
}
