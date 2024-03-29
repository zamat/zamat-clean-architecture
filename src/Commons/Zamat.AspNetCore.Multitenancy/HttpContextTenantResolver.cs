﻿using Microsoft.AspNetCore.Http;
using Zamat.Common.Multitenancy;

namespace Zamat.AspNetCore.Multitenancy;

class HttpContextTenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTenantResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Resolve()
    {
        var context = _httpContextAccessor.HttpContext!;
        if (!context.Items.TryGetValue(Constants.TenantKey, out object? item))
        {
            throw new TenantNotFoundException("Tenant context is required.");
        }

        if (item is null)
        {
            throw new TenantNotFoundException("Tenant context is required.");
        }

        return (string)item;
    }
}
