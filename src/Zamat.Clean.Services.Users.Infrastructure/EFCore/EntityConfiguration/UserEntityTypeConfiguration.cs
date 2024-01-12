using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamat.Clean.Infrastructure;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.Entities;

namespace Zamat.Clean.Services.Users.Infrastructure.EFCore.EntityConfiguration;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        _ = builder.ToTable("Users", Consts.DefaultDatabaseSchema);
        _ = builder.HasKey(x => x.Id);
    }
}
