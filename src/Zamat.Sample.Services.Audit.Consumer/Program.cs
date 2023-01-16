using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Audit.Consumer;
using Zamat.Sample.Services.Audit.Consumer.Consumers;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddOpenTelemetry(hostContext.Configuration, i =>
            {
                i.AddMassTransitInstrumentation();
            });

        services
            .AddEventHandlers()
            .ConfigureMessageBroker(hostContext.Configuration, c =>
            {
                c.AddConsumer<UserCreatedEventConsumer>();
            })
            .AddHealthChecks();
    })
    .Build();

await host.RunAsync();