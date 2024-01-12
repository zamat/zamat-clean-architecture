namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;

public sealed class AumsApiKeyAccess
{
    public Guid Id { get; private set; }

    public Guid ApiKeyId { get; private set; }

    public string ClientName { get; private set; }

    public string ClaimValue { get; private set; }
}
