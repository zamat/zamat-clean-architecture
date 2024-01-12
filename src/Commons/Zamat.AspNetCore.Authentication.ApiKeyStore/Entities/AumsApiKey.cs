namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;

public class AumsApiKey
{
    public Guid Id { get; private set; }

    public string Key { get; private set; }

    public string Owner { get; private set; }

    public IEnumerable<AumsApiKeyAccess> AumsAccess { get; } = new List<AumsApiKeyAccess>();
}
