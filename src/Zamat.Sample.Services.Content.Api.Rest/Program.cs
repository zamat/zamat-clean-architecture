using Microsoft.AspNetCore.Mvc;
using Zamat.AspNetCore.Diagnostics;
using Zamat.AspNetCore.Localization;
using Zamat.AspNetCore.Mvc.Rest;
using Zamat.AspNetCore.OpenAPI;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Content.Infrastructure;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddDataAnnotationsLocalization(o =>
    {
        o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(DataAnnotations));
    })
    .ConfigureWebAPIMvc();

builder.Services
    .AddEndpointsApiExplorer()
    .AddOpenApiDoc(builder.Configuration)
    .AddLocalization(builder.Configuration)
    .AddOpenTelemetry(builder.Configuration, i =>
    {
        i.AddEFCoreInstrumentation();
    })
    .AddHttpContextAccessor();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddHealthChecks(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}
else
{
    app.UseExceptionHandler("/api/error");
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRequestLocalization();

app.UseAuthorization();

app.UseDiagnostics(o =>
{
    o.AddTraceIdToResponseHeaders();
});

app.MapHealthChecks();
app.MapControllers();

app.Run();

public partial class Program { }
