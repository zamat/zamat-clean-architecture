using Microsoft.AspNetCore.HttpOverrides;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Zamat.AspNetCore.Diagnostics;
using Zamat.AspNetCore.Ocelot;
using Zamat.AspNetCore.OpenIddict;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Clean.ApiGateway.DelegatingHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
{
    o.FileOfSwaggerEndPoints = "ocelot.swagger";
    o.Folder = "Ocelot";
    o.HostEnvironment = builder.Environment;
    o.PrimaryOcelotConfigFileName = $"ocelot.{builder.Environment.EnvironmentName}.json";
});

builder.Services
    .AddOcelot()
    .AddDelegatingHandlers()
    .AddPolly()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddOpenTelemetry(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration, o =>
{
    o.RemoveParams("X-Tenant-Id");
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

app.UseSwaggerForOcelotUI();

app.UseOcelot().Wait();

app.UseDiagnostics(o =>
{
    o.AddTraceIdToResponseHeaders();
});

app.Run();

public partial class Program { }