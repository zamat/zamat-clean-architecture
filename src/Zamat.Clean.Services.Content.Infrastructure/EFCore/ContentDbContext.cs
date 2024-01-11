using Microsoft.EntityFrameworkCore;
using Zamat.Clean.Services.Content.Core.Entities;
using Zamat.Clean.Services.Content.Infrastructure.EFCore.EntityConfiguration;

namespace Zamat.Clean.Services.Content.Infrastructure.EFCore;

public class ContentDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; } = default!;

    public ContentDbContext(DbContextOptions<ContentDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
    }
}
