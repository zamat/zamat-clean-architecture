using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Audit.Worker.Consumers;
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
            .AddInfrastructure(hostContext.Configuration, c =>
            {
                c.AddConsumer<UserCreatedConsumer>();
            })
            .AddHealthChecks();
    })
    .Build();

await host.RunAsync();