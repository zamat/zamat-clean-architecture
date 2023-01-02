using Zamat.Common.Query;

namespace Zamat.Sample.Services.Users.Core.Queries.Users;

public record GetUserQuery(string Id) : IQuery<UserDto>
{
}
