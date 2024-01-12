namespace AUMS.AspNetCore.Authentication.ApiKey.Abstractions;

public interface IApiKeyStore
{
    Task<ApiKey?> GetApiKeyAsync(string apiKey);
}
