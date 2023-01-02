using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamat.Sample.Services.Users.Core.Domain.Entities;

namespace Zamat.Sample.Services.Users.Infrastructure.EFCore.EntityConfiguration;

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.ToTable("Users", "dbo");
        _ = builder.HasKey(x => x.Id);
        _ = builder.OwnsOne(x => x.FullName, b =>
        {
            b.Property(y => y.FirstName).HasColumnName("FirstName");
            b.Property(y => y.LastName).HasColumnName("LastName");
        });
    }
}
