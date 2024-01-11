using Zamat.Common.Query;

namespace Zamat.Clean.Services.Users.Core.Queries.Users;

public record GetUsersQuery(int Page, int Limit) : IQuery<IEnumerable<UserDto>>
{
}
