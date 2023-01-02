namespace Zamat.Sample.Services.Users.Core.Interfaces;
public interface IUsersQueries
{
    Task<User?> GetOrDefaultAsync(string userId, CancellationToken cancellationToken);
}
