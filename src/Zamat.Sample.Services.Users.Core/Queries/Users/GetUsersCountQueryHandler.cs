﻿using Zamat.Common.Query;

namespace Zamat.Sample.Services.Users.Core.Queries.Users;

class GetUsersCountQueryHandler : IQueryHandler<GetUsersCountQuery, int>
{
    private readonly IUsersQueries _usersQueries;

    public GetUsersCountQueryHandler(IUsersQueries usersQueries)
    {
        _usersQueries = usersQueries;
    }

    public async Task<QueryResult<int>> HandleAsync(GetUsersCountQuery query, CancellationToken cancellationToken)
    {
        return await _usersQueries.CountAsync(cancellationToken);
    }
}
