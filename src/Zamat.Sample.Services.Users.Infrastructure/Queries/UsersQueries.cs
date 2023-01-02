using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Zamat.Sample.Services.Users.Core.Domain.Entities;
using Zamat.Sample.Services.Users.Core.Interfaces;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;

namespace Zamat.Sample.Services.Users.Infrastructure.Queries;

class UsersQueries : IUsersQueries
{
    private readonly UsersDbContext _dbContext;

    public UsersQueries(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }
}
