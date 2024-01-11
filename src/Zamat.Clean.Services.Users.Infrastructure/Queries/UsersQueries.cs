using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Core.Interfaces;
using Zamat.Clean.Services.Users.Core.Queries.Users;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;

namespace Zamat.Clean.Services.Users.Infrastructure.Queries;

class UsersQueries(UsersDbContext dbContext) : IUsersQueries
{
    private readonly UsersDbContext _dbContext = dbContext;

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
