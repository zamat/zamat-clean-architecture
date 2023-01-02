using Zamat.BuildingBlocks.Domain;

namespace Zamat.Sample.Services.Users.Core.Domain.DomainEvents;

public record UserCreatedEvent(string Id, string UserName) : IDomainEvent
{
}
