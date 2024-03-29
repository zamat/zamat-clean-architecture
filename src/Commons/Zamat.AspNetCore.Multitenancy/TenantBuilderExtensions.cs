﻿using Zamat.Common.Multitenancy;

namespace Zamat.AspNetCore.Multitenancy;

public static class TenantBuilderExtensions
{
    public static TenantBuilder AddHttpContextResolver(this TenantBuilder tenantBuilder)
    {
        tenantBuilder.AddResolver<HttpContextTenantResolver>();

        return tenantBuilder;
    }
}
