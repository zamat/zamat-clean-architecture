using Zamat.Common.Query;

namespace Zamat.Clean.Services.Users.Core.Queries.Users;

class GetUsersQueryHandler(IUsersQueries usersQueries) : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUsersQueries _usersQueries = usersQueries;

    public async Task<QueryResult<IEnumerable<UserDto>>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = _usersQueries.GetAsync(query, cancellationToken);

        var dtos = new List<UserDto>();

        await foreach (var user in users)
        {
            dtos.Add(new UserDto(user));
        }

        return dtos;
    }
}
