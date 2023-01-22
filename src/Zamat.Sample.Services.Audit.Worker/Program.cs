using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Audit.Worker.Api.Consumers;
using Zamat.Sample.Services.Audit.Worker.Core;
using Zamat.Sample.Services.Audit.Worker.Infrastructure;

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
            .AddCoreBuildingBlocks()
            .AddInfrastructure(hostContext.Configuration, c =>
            {
                c.AddConsumer<UserCreatedConsumer>();
            })
            .AddHealthChecks();
    })
    .Build();

await host.RunAsync();