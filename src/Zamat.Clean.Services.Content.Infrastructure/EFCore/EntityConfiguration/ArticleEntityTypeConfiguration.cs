using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamat.Clean.Services.Content.Core.Entities;

namespace Zamat.Clean.Services.Content.Infrastructure.EFCore.EntityConfiguration;

class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        _ = builder.ToTable("Articles", "dbo");
        _ = builder.HasKey(x => x.Id);
    }
}
