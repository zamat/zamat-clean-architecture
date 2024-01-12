using System.Threading.Tasks;
using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Multitenancy;

internal class CookieMiddleware
{
    private readonly RequestDelegate _next;

    public CookieMiddleware(RequestDelegate next)
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

        var result = await context.AuthenticateAsync();

        if (!result.Succeeded)
        {
            await _next(context);
            return;
        }

        var claim = result.Principal.FindFirst(Constants.RealmQueryParam);
        if (claim is null)
        {
            throw new TenantNotFoundException($"Authentication context require realm claim.");
        }

        var tenant = await tenantStore.GetTenantByRealmAsync(claim.Value);
        if (tenant is not null)
        {
            context.Items.Add(Constants.TenantKey, tenant);
        }

        await _next(context);
    }
}
