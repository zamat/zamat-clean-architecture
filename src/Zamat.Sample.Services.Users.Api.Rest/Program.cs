using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Zamat.AspNetCore.Diagnostics;
using Zamat.AspNetCore.Localization;
using Zamat.AspNetCore.Mvc.Rest;
using Zamat.AspNetCore.OpenAPI;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Users.Api.Rest.Consumers;
using Zamat.Sample.Services.Users.Core;
using Zamat.Sample.Services.Users.Infrastructure;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(o =>
    {
        o.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    })
    .AddDataAnnotationsLocalization(o =>
    {
        o.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(DataAnnotations));
    })
    .ConfigureWebAPIMvc();

builder.Services
    .AddEndpointsApiExplorer()
    .AddProblemDetails()
    .AddOpenApiDoc(builder.Configuration)
    .AddLocalization(builder.Configuration)
    .AddOpenTelemetry(builder.Configuration, i =>
    {
        i.AddMassTransitInstrumentation();
        i.AddEFCoreInstrumentation();
    })
    .AddHttpContextAccessor();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration, c =>
    {
        c.AddConsumer<UserCreatedEventConsumer>();
    })
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