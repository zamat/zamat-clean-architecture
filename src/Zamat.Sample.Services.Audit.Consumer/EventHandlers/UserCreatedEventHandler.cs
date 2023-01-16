using MassTransit;
using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Users.Core.Events;

namespace Zamat.Sample.Services.Audit.Consumer.EventHandlers;

class UserCreatedEventHandler : IEventHandler<UserCreated>, IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserCreated> context)
    {
        return HandleAsync(context.Message, context.CancellationToken);
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User created event handled (Event : {eventData})", @event);

        await Task.Delay(50, cancellationToken);
    }
}
