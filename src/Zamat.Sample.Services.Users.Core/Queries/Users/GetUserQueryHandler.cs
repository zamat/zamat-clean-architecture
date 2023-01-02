using Zamat.Common.Query;
using Zamat.Sample.Services.Users.Core.Interfaces;

namespace Zamat.Sample.Services.Users.Core.Queries.Users;

class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUsersQueries _usersQueries;

    public GetUserQueryHandler(IUsersQueries usersQueries)
    {
        _usersQueries = usersQueries;
    }

    public async Task<QueryResult<UserDto>> HandleAsync(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersQueries.GetOrDefaultAsync(query.Id, cancellationToken);
        if (user is null)
        {
            return new QueryResult<UserDto>(new QueryError(QueryErrorCode.UserNotFound, "User with given id not found."));
        }

        return new UserDto(user);
    }
}
