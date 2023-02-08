using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zamat.Sample.Services.Content.Core.Entities;

namespace Zamat.Sample.Services.Content.Infrastructure.EFCore.EntityConfiguration;

class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        _ = builder.ToTable("Articles", "dbo");
        _ = builder.HasKey(x => x.Id);
    }
}
