using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zamat.BuildingBlocks.Domain.Specifications;
using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Core.Domain.Repositories;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;

namespace Zamat.Clean.Services.Users.Infrastructure.Repositories;

class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
    }

    public Task<bool> CheckExistsAsync(Specification<User> specification, CancellationToken cancellationToken)
    {
        return _dbContext.Users.AnyAsync(specification.ToExpression(), cancellationToken);
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Remove(user);
        return Task.CompletedTask;
    }

    public Task<User> GetAsync(Specification<User> specification, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstAsync(specification.ToExpression(), cancellationToken)!;
    }

    public Task<User?> GetOrDefaultAsync(Specification<User> specification, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);
    }
}
