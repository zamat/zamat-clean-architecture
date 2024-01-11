using Zamat.BuildingBlocks.Domain;
using Zamat.Clean.Services.Users.Core.Domain.Aggregates;

namespace Zamat.Clean.Services.Users.Core.Domain.Repositories;

public interface IUserRepository : IRepository<User, string>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
}
