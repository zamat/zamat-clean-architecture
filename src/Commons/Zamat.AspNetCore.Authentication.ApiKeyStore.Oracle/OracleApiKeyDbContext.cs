using AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;
using AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle;

internal sealed class OracleApiKeyDbContext : DbContext
{
    public DbSet<AumsApiKey> ApiKeys { get; set; } = default!;

    public OracleApiKeyDbContext(DbContextOptions<OracleApiKeyDbContext> dbContextOptions)
        : base(dbContextOptions) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ApiKeyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AumsAccessEntityConfiguration());
    }
}
