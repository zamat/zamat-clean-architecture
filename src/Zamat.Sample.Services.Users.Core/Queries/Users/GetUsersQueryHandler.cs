using Zamat.Common.Query;

namespace Zamat.Sample.Services.Users.Core.Queries.Users;

class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUsersQueries _usersQueries;

    public GetUsersQueryHandler(IUsersQueries usersQueries)
    {
        _usersQueries = usersQueries;
    }

    public async Task<QueryResult<IEnumerable<UserDto>>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _usersQueries.GetAsync(query, cancellationToken);

        var dtos = new List<UserDto>();

        foreach (var user in users)
        {
            dtos.Add(new UserDto(user));
        }

        return dtos;
    }
}
