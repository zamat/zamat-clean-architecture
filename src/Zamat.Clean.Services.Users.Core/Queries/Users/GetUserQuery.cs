using Zamat.Common.Query;

namespace Zamat.Clean.Services.Users.Core.Queries.Users;

public record GetUserQuery(string Id) : IQuery<UserDto>
{
}
