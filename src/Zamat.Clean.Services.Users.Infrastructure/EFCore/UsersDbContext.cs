using MassTransit;
using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.EntityConfiguration;

namespace Zamat.Clean.Services.Users.Infrastructure.EFCore;

public class UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity(c => c.ToTable("InboxStates", "dbo"));
        modelBuilder.AddOutboxMessageEntity(c => c.ToTable("OutboxMessages", "dbo"));
        modelBuilder.AddOutboxStateEntity(c => c.ToTable("OutboxStates", "dbo"));

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
