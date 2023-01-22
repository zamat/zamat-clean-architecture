using Zamat.AspNetCore.Diagnostics;
using Zamat.AspNetCore.Grpc;
using Zamat.AspNetCore.Localization;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Users.Api.Grpc.Services.v1;
using Zamat.Sample.Services.Users.Core;
using Zamat.Sample.Services.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(o =>
{
    o.Interceptors.AddLoggerInterceptor();
});

builder.Services
    .AddLocalization(builder.Configuration)
    .AddOpenTelemetry(builder.Configuration, i =>
    {
        i.AddMassTransitInstrumentation();
        i.AddEFCoreInstrumentation();
    })
    .AddHttpContextAccessor();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddHealthChecks(builder.Configuration);

var app = builder.Build();

app.UseDiagnostics(o =>
{
    o.AddTraceIdToResponseHeaders();
});

app.MapGrpcService<UsersService>();

app.Run();

public partial class Program { }
