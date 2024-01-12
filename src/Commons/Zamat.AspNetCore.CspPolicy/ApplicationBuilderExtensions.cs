using Microsoft.AspNetCore.Builder;

namespace AUMS.AspNetCore.CspPolicy;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCspMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CspMiddleware>();
    }
}
