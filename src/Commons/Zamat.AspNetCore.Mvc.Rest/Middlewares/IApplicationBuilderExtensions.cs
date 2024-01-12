using Microsoft.AspNetCore.Builder;

namespace Zamat.AspNetCore.Mvc.Rest.Middlewares;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestBuffering(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestBufferingMiddleware>();
    }
}
