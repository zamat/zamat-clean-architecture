using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.CspPolicy;

internal class CspMiddleware
{
    private readonly RequestDelegate _next;

    public CspMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext httpContext, IOptions<CspOptions> options)
    {
        var cspOptions = options.Value;

        if (!httpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
        {
            httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        }

        if (!string.IsNullOrEmpty(cspOptions.Csp))
        {
            if (!httpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                httpContext.Response.Headers.Add("Content-Security-Policy", cspOptions.Csp);
            }

            if (!httpContext.Response.Headers.ContainsKey("X-Content-Security-Policy"))
            {
                httpContext.Response.Headers.Add("X-Content-Security-Policy", cspOptions.Csp);
            }
        }

        return _next(httpContext);
    }
}
