using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace Zamat.AspNetCore.Diagnostics;

public static class ApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder MapHealthChecks(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("ready")
        });

        app.MapHealthChecks("/healthz/live");

        return app;
    }

    public static IApplicationBuilder UseDiagnostics(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AddTraceIdMiddleware>();
    }
}
