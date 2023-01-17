using Zamat.Common.Events;

namespace Zamat.Sample.Services.Users.Core.IntegrationEvents;

public record UserCreated(string Id, string UserName) : IEvent
{
    public DateTimeOffset OccuredAt { get; init; } = DateTimeOffset.UtcNow;
}
