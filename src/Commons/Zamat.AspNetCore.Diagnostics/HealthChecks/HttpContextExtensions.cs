using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Zamat.AspNetCore.Diagnostics.HealthChecks;

internal static class HttpContextExtensions
{
    public static async Task<HttpContext> WriteHealthReportAsync(this HttpContext httpContext, HealthReport report)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        var result = new HealthCheckResponse(report.Status);
        foreach (var entry in report.Entries)
        {
            result.Add(entry.Key, new HealthCheckEntryResponse(entry.Value));
        }

        await JsonSerializer.SerializeAsync(httpContext.Response.Body, result, CreateJsonOptions()).ConfigureAwait(false);

        return httpContext;
    }

    internal static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}
