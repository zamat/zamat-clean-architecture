using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using System;
using System.IO;

namespace Zamat.AspNetCore.Ocelot;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerForOcelotUI(this IApplicationBuilder app, Action<SwaggerDefinition> configureDefinition)
    {
        var definition = new SwaggerDefinition();
        configureDefinition(definition);

        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.ReConfigureUpstreamSwaggerJson = (httpContext, json) =>
            {
                using var outputString = new StringWriter();
                var openApiDocument = new OpenApiStringReader().Read(json, out OpenApiDiagnostic diagnostic);
                if (openApiDocument is null)
                    return string.Empty;
                foreach (var param in definition.ParamsToRemove)
                    openApiDocument.RemoveParam(param);
                openApiDocument.SerializeAsV3(new OpenApiJsonWriter(outputString));
                return outputString.ToString();
            };
        });
        return app;
    }
}
