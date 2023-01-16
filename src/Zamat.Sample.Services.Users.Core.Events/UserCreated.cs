using Zamat.Common.Events;

namespace Zamat.Sample.Services.Users.Core.Events;

public record UserCreated(string Id, string UserName) : IEvent
{
    public DateTimeOffset OccuredAt { get; init; } = DateTimeOffset.UtcNow;
}
