using Microsoft.AspNetCore.HttpOverrides;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Zamat.AspNetCore.Diagnostics;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Clean.Api.Gateway.DelegatingHandlers;

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

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
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