using AUMS.AspNetCore.Authentication.ApiKeyStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle.EntityConfiguration;

internal sealed class ApiKeyEntityConfiguration : IEntityTypeConfiguration<AumsApiKey>
{
    public void Configure(EntityTypeBuilder<AumsApiKey> builder)
    {
        builder.ToTable("FR_API_KEY_STORE");
        builder.HasKey(e => e.Id);
        builder.Property(b => b.Id).HasColumnName(@"ID_API_KEY_STORE");
        builder.Property(b => b.Key).HasColumnName(@"KEY");
        builder.Property(a => a.Owner).HasColumnName(@"OWNER");

        builder
            .HasMany(a => a.AumsAccess)
            .WithOne()
            .HasForeignKey(pa => pa.ApiKeyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
