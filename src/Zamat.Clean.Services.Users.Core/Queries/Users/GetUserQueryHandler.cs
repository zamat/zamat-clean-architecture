using Zamat.Common.Query;

namespace Zamat.Clean.Services.Users.Core.Queries.Users;

internal class GetUserQueryHandler(IUsersQueries usersQueries) : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUsersQueries _usersQueries = usersQueries;

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
