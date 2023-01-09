using Microsoft.AspNetCore.Builder;

namespace Zamat.AspNetCore.Mvc.Rest;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AddTraceIdMiddleware>();
    }
}
