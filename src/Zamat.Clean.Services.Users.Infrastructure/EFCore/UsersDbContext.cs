using MassTransit;
using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Infrastructure;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.Entities;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.EntityConfiguration;

namespace Zamat.Clean.Services.Users.Infrastructure.EFCore;

public class UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<UserEntity> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Consts.DefaultDatabaseSchema);

        modelBuilder.AddInboxStateEntity(c => c.ToTable("InboxStates", Consts.DefaultDatabaseSchema));
        modelBuilder.AddOutboxMessageEntity(c => c.ToTable("OutboxMessages", Consts.DefaultDatabaseSchema));
        modelBuilder.AddOutboxStateEntity(c => c.ToTable("OutboxStates", Consts.DefaultDatabaseSchema));

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
