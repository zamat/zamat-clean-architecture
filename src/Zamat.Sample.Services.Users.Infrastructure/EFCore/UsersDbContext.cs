using MassTransit;
using Microsoft.EntityFrameworkCore;
using Zamat.Sample.Services.Users.Core.Domain.Entities;
using Zamat.Sample.Services.Users.Infrastructure.EFCore.EntityConfiguration;

namespace Zamat.Sample.Services.Users.Infrastructure.EFCore;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;

    public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity(c => c.ToTable("InboxStates", "dbo"));
        modelBuilder.AddOutboxMessageEntity(c => c.ToTable("OutboxMessages", "dbo"));
        modelBuilder.AddOutboxStateEntity(c => c.ToTable("OutboxStates", "dbo"));

        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
