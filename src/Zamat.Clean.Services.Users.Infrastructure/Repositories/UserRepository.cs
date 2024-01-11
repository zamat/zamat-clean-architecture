using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Users.Core.Domain.Aggregates;
using Zamat.Clean.Services.Users.Core.Domain.Repositories;
using Zamat.Clean.Services.Users.Core.Domain.ValueObjects;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.Entities;

namespace Zamat.Clean.Services.Users.Infrastructure.Repositories;

internal class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    private readonly UsersDbContext _dbContext = dbContext;

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        var entity = new UserEntity()
        {
            Id = user.Id,
            FirstName = user.FullName.FirstName,
            LastName = user.FullName.LastName,
            UserName = user.UserName
        };

        await _dbContext.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        if (entity is null)
        {
            return;
        }

        _dbContext.Remove(entity);
    }

    public async Task<User?> GetAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (entity is null)
        {
            return default;
        }

        return new User(entity.Id, entity.UserName, new FullName(entity.FirstName, entity.LastName));
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        if (entity is null)
        {
            return default;
        }

        return new User(entity.Id, entity.UserName, new FullName(entity.FirstName, entity.LastName));
    }
}
