using Zamat.Clean.Services.Users.Core.Queries.Users;

namespace Zamat.Clean.Services.Users.Core.Interfaces;
public interface IUsersQueries
{
    Task<UserDto?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAsync(GetUsersQuery query, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
}
