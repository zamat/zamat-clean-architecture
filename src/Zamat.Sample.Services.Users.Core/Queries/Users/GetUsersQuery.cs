using Zamat.Common.Query;

namespace Zamat.Sample.Services.Users.Core.Queries.Users;

public record GetUsersQuery(int Page, int Limit) : IQuery<IEnumerable<UserDto>>
{
}
