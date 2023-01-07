using MassTransit;
using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Users.Core.IntegrationEvents.Users;

namespace Zamat.Sample.Services.Users.Api.Rest.Consumers;

class UserCreatedEventConsumer : IConsumer<UserCreated>
{
    private readonly IEventHandler<UserCreated> _eventHandler;
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(IEventHandler<UserCreated> eventHandler, ILogger<UserCreatedEventConsumer> logger)
    {
        _eventHandler = eventHandler;
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogDebug("User created event received (Event : {eventData})", context.Message);

        return _eventHandler.HandleAsync(context.Message, context.CancellationToken);
    }
}
