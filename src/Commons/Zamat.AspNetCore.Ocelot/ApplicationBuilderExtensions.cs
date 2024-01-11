using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Zamat.AspNetCore.OpenAPI;

namespace Zamat.AspNetCore.Ocelot;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerForOcelotUI(this IApplicationBuilder app)
    {
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.ReConfigureUpstreamSwaggerJson = (httpContext, json) =>
            {
                var overrideDefinitionOptions = httpContext.RequestServices.GetRequiredService<IOptions<SwaggerDefinitionOverride>>();

                var overrideDefinition = overrideDefinitionOptions.Value;

                using var outputString = new StringWriter();
                var openApiDocument = new OpenApiStringReader().Read(json, out OpenApiDiagnostic diagnostic);
                if (openApiDocument is null)
                    return string.Empty;

                foreach (var param in overrideDefinition.ParamsToRemove)
                {
                    openApiDocument.RemoveParam(param);
                }

                openApiDocument.SerializeAsV3(new OpenApiJsonWriter(outputString));
                return outputString.ToString();
            };
        });

        return app;
    }
}
