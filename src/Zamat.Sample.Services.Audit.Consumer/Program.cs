using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Sample.Services.Audit.Consumer;
using Zamat.Sample.Services.Audit.Consumer.EventHandlers;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddOpenTelemetry(hostContext.Configuration, i =>
            {
                i.AddMassTransitInstrumentation();
            });

        services
            .ConfigureMessageBroker(hostContext.Configuration, c =>
            {
                c.AddConsumer<UserCreatedEventHandler>();
            })
            .AddHealthChecks();
    })
    .Build();

await host.RunAsync();