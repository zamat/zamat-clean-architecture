using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Clean.Services.Audit.Worker.Api.Consumers;
using Zamat.Clean.Services.Audit.Worker.Core;
using Zamat.Clean.Services.Audit.Worker.Infrastructure;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddOpenTelemetry(hostContext.Configuration, i =>
            {
                i.AddMassTransitInstrumentation();
            });

        services
            .AddCore()
            .AddInfrastructure(hostContext.Configuration, c =>
            {
                c.AddConsumer<UserCreatedConsumer>();
            })
            .AddHealthChecks();
    })
    .Build();

await host.RunAsync();