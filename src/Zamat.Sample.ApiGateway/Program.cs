using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.IO;
using System.Text.Json.Serialization;
using Zamat.AspNetCore.Localization;
using Zamat.AspNetCore.OpenAPI;
using Zamat.AspNetCore.OpenIddict;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOcelot()
    .AddPolly()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLocalization(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration, (opt) =>
{
    opt.GenerateDocsForGatewayItSelf = true;
    opt.GenerateDocsForAggregates = true;
    opt.AggregateDocsGeneratorPostProcess = (aggregateRoute, routesDocs, pathItemDoc, documentation) =>
    {
        pathItemDoc.RemoveParam("X-Tenant-Id");
        documentation.RemoveParam("X-Tenant-Id");
    };
});

builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
{
    o.FileOfSwaggerEndPoints = "ocelot.swagger";
    o.Folder = "Ocelot";
    o.HostEnvironment = builder.Environment;
    o.PrimaryOcelotConfigFileName = $"ocelot.{builder.Environment.EnvironmentName}.json";
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddOidcAuthentication(o =>
{
    builder.Configuration.GetSection(nameof(OidcAuthOptions)).Bind(o);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors();

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.ReConfigureUpstreamSwaggerJson = (httpContext, json) =>
    {
        using var outputString = new StringWriter();
        var openApiDocument = new OpenApiStringReader().Read(json, out OpenApiDiagnostic diagnostic);
        if (openApiDocument is null)
            return string.Empty;
        openApiDocument.RemoveParam("X-Tenant-Id");
        openApiDocument.SerializeAsV3(new OpenApiJsonWriter(outputString));
        return outputString.ToString();
    };
});

app.UseOcelot().Wait();

app.Run();

public partial class Program { }