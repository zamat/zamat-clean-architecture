using Microsoft.AspNetCore.Builder;
using Zamat.AspNetCore.OpenAPI;

namespace Zamat.AspNetCore.OpenAPI;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger(setupAction: null);
        app.UseSwaggerUI();
        return app;
    }
}
