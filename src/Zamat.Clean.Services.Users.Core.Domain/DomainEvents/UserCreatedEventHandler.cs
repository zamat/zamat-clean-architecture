using Zamat.BuildingBlocks.Domain;

namespace Zamat.Clean.Services.Users.Core.Domain.DomainEvents;

public class UserCreatedEventHandler : IDomainEventHandler<UserCreatedEvent>
{
    public Task HandleAsync(UserCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
