﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zamat.Sample.Services.Users.Core.Domain.Entities;
using Zamat.Sample.Services.Users.Core.Interfaces;
using Zamat.Sample.Services.Users.Core.Queries.Users;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;

namespace Zamat.Sample.Services.Users.Infrastructure.Queries;

class UsersQueries : IUsersQueries
{
    private readonly UsersDbContext _dbContext;

    public UsersQueries(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Users.CountAsync(cancellationToken);
    }

    public IAsyncEnumerable<User> GetAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return _dbContext.Users
            .OrderBy(x => x.Id)
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit)
            .AsAsyncEnumerable();
    }

    public Task<User?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }
}
