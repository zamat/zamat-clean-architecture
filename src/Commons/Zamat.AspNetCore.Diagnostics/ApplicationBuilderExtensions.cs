using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Zamat.AspNetCore.Diagnostics.HealthChecks;

namespace Zamat.AspNetCore.Diagnostics;

public static class ApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder MapHealthChecks(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/healthz/ready", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("ready"),
            ResponseWriter = WriteHealthCheckResponse
        });

        app.MapHealthChecks("/healthz/live", new HealthCheckOptions
        {
            ResponseWriter = WriteHealthCheckResponse
        });

        return app;
    }

    public static IApplicationBuilder UseDiagnostics(this IApplicationBuilder builder, Action<DiagnosticsOptions> optionsConfigure)
    {
        var opt = new DiagnosticsOptions();
        optionsConfigure(opt);
        if (opt.UseTraceIdResponseHeader)
        {
            builder.UseMiddleware<AddTraceIdMiddleware>();
        }
        return builder;
    }

    private static Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
    {
        return httpContext.WriteHealthReportAsync(report);
    }
}
