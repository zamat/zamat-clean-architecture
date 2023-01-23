using MassTransit;
using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Users.Core.IntegrationEvents;

namespace Zamat.Sample.Services.Audit.Worker.Api.Consumers;

class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly IEventHandler<UserCreated> _eventHandler;
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(IEventHandler<UserCreated> eventHandler, ILogger<UserCreatedConsumer> logger)
    {
        _eventHandler = eventHandler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation("User created event received (Event : {eventData})", context.Message);

        await _eventHandler.HandleAsync(context.Message, context.CancellationToken);
    }
}
