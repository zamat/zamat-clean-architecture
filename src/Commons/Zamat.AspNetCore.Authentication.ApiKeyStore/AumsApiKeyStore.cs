using System.Security.Claims;
using AUMS.AspNetCore.Authentication.ApiKey.Abstractions;
using AUMS.AspNetCore.Authentication.ApiKeyStore.Repositories;
using AUMS.AspNetCore.Authorization.Claims;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore;

internal sealed class AumsApiKeyStore : IApiKeyStore
{
    private readonly IAumsApiKeyRepository _keyRepository;

    public AumsApiKeyStore(IAumsApiKeyRepository keyRepository)
    {
        _keyRepository = keyRepository;
    }

    public async Task<ApiKey.ApiKey?> GetApiKeyAsync(string apiKeyValue)
    {
        var aumsApiKey = await _keyRepository.GetAsync(apiKeyValue, GetCancellationToken());

        if (aumsApiKey is null)
        {
            return null;
        }

        var apiKey = new ApiKey.ApiKey(apiKeyValue, aumsApiKey.Owner);

        foreach (var aumsAccess in aumsApiKey.AumsAccess)
        {
            apiKey.AddClaim(new Claim(AumsClaimTypes.AumsAccess, aumsAccess.ClaimValue));
        }

        return apiKey;
    }

    private static CancellationToken GetCancellationToken()
    {
        CancellationTokenSource cancellationTokenSource = new();

        return cancellationTokenSource.Token;
    }
}
