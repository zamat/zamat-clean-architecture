using AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Repositories;

public interface IAumsApiKeyRepository
{
    Task<AumsApiKey?> GetAsync(string keyId, CancellationToken cancellationToken);
}
