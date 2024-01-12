using AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle.EntityConfiguration;

internal sealed class AumsAccessEntityConfiguration : IEntityTypeConfiguration<AumsApiKeyAccess>
{
    public void Configure(EntityTypeBuilder<AumsApiKeyAccess> builder)
    {
        builder.ToTable("FR_API_KEY_STORE_ACCESS");
        builder.Property(b => b.Id).HasColumnName(@"ID_API_KEY_STORE_ACCESS");
        builder.Property(b => b.ApiKeyId).HasColumnName(@"ID_API_KEY_STORE");
        builder.Property(a => a.ClientName).HasColumnName(@"CLIENT_NAME");
        builder.Property(a => a.ClaimValue).HasColumnName(@"CLAIM_VALUE");
        builder.HasKey(e => e.Id);
    }
}
