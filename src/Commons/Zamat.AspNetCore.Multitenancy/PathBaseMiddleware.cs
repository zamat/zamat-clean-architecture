using System;
using System.Linq;
using System.Threading.Tasks;
using AUMS.Common.Multitenancy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace AUMS.AspNetCore.Multitenancy;

public class PathBaseMiddleware
{
    private readonly RequestDelegate _next;

    public PathBaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITenantStore tenantStore)
    {
        var originalPath = context.Request.Path;

        var originalPathBase = context.Request.PathBase;

        (string? realm, string? realPath) = GetTenantAndPathFrom(context.Request);

        if (string.IsNullOrEmpty(realm))
        {
            await _next(context);
            return;
        }

        var tenant = await tenantStore.GetTenantByRealmAsync(realm);
        if (tenant is not null)
        {
            context.Request.Path = realPath;
            context.Request.PathBase = originalPathBase.Add($"/{realm}");

            context.Items.Add(Constants.TenantKey, tenant);
        }

        await _next(context);
    }

    private static (string? realm, string? realPath) GetTenantAndPathFrom(HttpRequest httpRequest)
    {
        var realm = new Uri(httpRequest.GetDisplayUrl()).Segments.FirstOrDefault(x => x != "/")?.TrimEnd('/');

        if (!string.IsNullOrWhiteSpace(realm) && httpRequest.Path.StartsWithSegments($"/{realm}", out PathString realPath))
        {
            return (realm, realPath);
        }

        return (null, null);
    }
}
