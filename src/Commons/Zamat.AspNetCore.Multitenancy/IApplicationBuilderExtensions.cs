using System;
using Microsoft.AspNetCore.Builder;

namespace AUMS.AspNetCore.Multitenancy;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder, Action<MultitenancyOptions> configure)
    {
        var opt = new MultitenancyOptions();
        configure(opt);

        _ = opt.ResolvingStategy switch
        {
            ResolvingStategy.Host => builder.UseMiddleware<HostMiddleware>(),
            ResolvingStategy.Header => builder.UseMiddleware<HeaderMiddleware>(),
            ResolvingStategy.Subdomain => builder.UseMiddleware<SubdomainMiddleware>(),
            ResolvingStategy.Query => builder.UseMiddleware<QueryMiddleware>(),
            ResolvingStategy.PathBase => builder.UseMiddleware<PathBaseMiddleware>(),
            ResolvingStategy.CookieOrQuery => builder
            .UseMiddleware<CookieMiddleware>()
            .UseMiddleware<QueryMiddleware>(),
            _ => throw new NotImplementedException(),
        };

        return builder;
    }
}
