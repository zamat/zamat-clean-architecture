using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace AUMS.AspNetCore.BackendForFrontend.Abstractions;

public interface IRefreshTokenService
{
    Task<TokenResponse> TryRefreshTokenAsync(string refreshToken, string expiresAt, CancellationToken ct);
}
