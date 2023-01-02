using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Zamat.AspNetCore.Localization;
using Zamat.AspNetCore.OpenAPI;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Users.Api.Rest;
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
    });

builder.Services.AddProblemDetails();

builder.Services
    .AddEndpointsApiExplorer()
    .AddOpenApiDoc(builder.Configuration)
    .AddLocalization(builder.Configuration)
    .AddOpenTelemetry(builder.Configuration)
    .AddHttpContextAccessor()
    .AddProblemFactory();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/api/error");
}

app.UseRequestLocalization();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }