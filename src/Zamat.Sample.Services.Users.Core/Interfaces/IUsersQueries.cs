using Zamat.Sample.Services.Users.Core.Queries.Users;

namespace Zamat.Sample.Services.Users.Core.Interfaces;
public interface IUsersQueries
{
    Task<User?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken);
    IAsyncEnumerable<User> GetAsync(GetUsersQuery query, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
}
