using Zamat.Clean.Services.Users.Core.Queries.Users;

namespace Zamat.Clean.Services.Users.Core.Interfaces;
public interface IUsersQueries
{
    Task<User?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken);
    IAsyncEnumerable<User> GetAsync(GetUsersQuery query, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
}
