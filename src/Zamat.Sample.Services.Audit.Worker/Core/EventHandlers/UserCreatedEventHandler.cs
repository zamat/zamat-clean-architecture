using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Users.Core.IntegrationEvents;

namespace Zamat.Sample.Services.Audit.Worker.Core.EventHandlers;

class UserCreatedEventHandler : IEventHandler<UserCreated>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User created event handled (Event : {eventData})", @event);

        await Task.Delay(50, cancellationToken);
    }
}
