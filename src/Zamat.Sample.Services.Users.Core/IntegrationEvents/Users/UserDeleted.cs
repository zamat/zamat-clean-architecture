using Zamat.Common.Events;

namespace Zamat.Sample.Services.Users.Core.IntegrationEvents.Users;

public record UserDeleted(string Id, string UserName) : IEvent
{
    public DateTimeOffset OccuredAt { get; init; } = DateTimeOffset.UtcNow;

    internal UserDeleted(User user) : this(user.Id, user.UserName)
    {

    }
}
