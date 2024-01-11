using Zamat.BuildingBlocks.Domain;
using Zamat.Clean.Services.Users.Core.Domain.DomainEvents;

namespace Zamat.Clean.Services.Users.Core.Domain.DomainEventHandlers;

public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
    }
}
