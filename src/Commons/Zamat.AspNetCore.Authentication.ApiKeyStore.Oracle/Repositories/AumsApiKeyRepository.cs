using AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;
using AUMS.AspNetCore.Authentication.ApiKeyStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle.Repositories;

internal sealed class AumsApiKeyRepository : IAumsApiKeyRepository
{
    private readonly OracleApiKeyDbContext _dbContext;

    public AumsApiKeyRepository(OracleApiKeyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<AumsApiKey?> GetAsync(string keyId, CancellationToken cancellationToken)
    {
        return _dbContext.ApiKeys
            .Include(i => i.AumsAccess)
            .AsNoTracking()
            .AsSingleQuery()
            .FirstOrDefaultAsync(q => q.Key == keyId, cancellationToken: cancellationToken);
    }
}
