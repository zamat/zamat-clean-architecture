using Microsoft.Extensions.Logging;
using Zamat.Common.Events.Bus;

namespace Zamat.Sample.Services.Users.Api.Rest.IntegrationEvents;

class UserCreatedEventHandler : IEventHandler<UserCreated>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        _logger.LogDebug("User created event processing (Event : {eventData})", @event);

        await Task.Delay(50, cancellationToken);
    }
}
