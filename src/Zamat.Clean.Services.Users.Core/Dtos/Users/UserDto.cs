using Zamat.Clean.Services.Users.Core.Domain.Aggregates;

namespace Zamat.Clean.Services.Users.Core.Dtos.Users;

public record UserDto(string Id, string UserName, string FirstName, string LastName)
{
    public UserDto(User user) : this(new UserDto(
        user.Id,
        user.UserName,
        user.FullName.FirstName,
        user.FullName.LastName
        ))
    {
    }
}
