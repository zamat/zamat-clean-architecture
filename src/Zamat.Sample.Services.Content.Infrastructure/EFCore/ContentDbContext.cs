using Microsoft.EntityFrameworkCore;
using Zamat.Sample.Services.Content.Core.Entities;
using Zamat.Sample.Services.Content.Infrastructure.EFCore.EntityConfiguration;

namespace Zamat.Sample.Services.Content.Infrastructure.EFCore;

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
