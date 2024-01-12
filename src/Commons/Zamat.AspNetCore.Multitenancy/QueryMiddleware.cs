using System.Threading.Tasks;
using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Multitenancy;

internal class QueryMiddleware
{
    private readonly RequestDelegate _next;

    public QueryMiddleware(RequestDelegate next)
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

        if (!context.Request.Query.TryGetValue(Constants.RealmQueryParam, out var value))
        {
            await _next(context);
            return;
        }

        var tenant = await tenantStore.GetTenantByRealmAsync(value);
        if (tenant is not null)
        {
            context.Items.Add(Constants.TenantKey, tenant);
        }

        await _next(context);
    }
}
