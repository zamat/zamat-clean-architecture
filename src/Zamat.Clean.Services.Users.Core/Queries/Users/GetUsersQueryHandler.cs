using Zamat.Common.Query;

namespace Zamat.Clean.Services.Users.Core.Queries.Users;

internal class GetUsersQueryHandler(IUsersQueries usersQueries) : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUsersQueries _usersQueries = usersQueries;

    public async Task<QueryResult<IEnumerable<UserDto>>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _usersQueries.GetAsync(query, cancellationToken);

        return new QueryResult<IEnumerable<UserDto>>() { Result = users };
    }
}
