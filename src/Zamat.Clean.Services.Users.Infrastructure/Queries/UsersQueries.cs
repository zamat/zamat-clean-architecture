using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Users.Core.Dtos.Users;
using Zamat.Clean.Services.Users.Core.Interfaces;
using Zamat.Clean.Services.Users.Core.Queries.Users;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;

namespace Zamat.Clean.Services.Users.Infrastructure.Queries;

internal class UsersQueries(UsersDbContext dbContext) : IUsersQueries
{
    private readonly UsersDbContext _dbContext = dbContext;

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Users.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserDto>> GetAsync(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .OrderBy(x => x.Id)
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit)
            .Select(entity => new UserDto(entity.Id, entity.UserName, entity.FirstName, entity.LastName))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<UserDto?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (entity is null)
        {
            return null;
        }
        return new UserDto(entity.Id, entity.UserName, entity.FirstName, entity.LastName);
    }
}
