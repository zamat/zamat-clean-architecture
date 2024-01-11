using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zamat.Clean.Services.Content.Infrastructure.EFCore;

namespace EFCore.PostgreSQL;

class ContentDbContextFactory : IDesignTimeDbContextFactory<ContentDbContext>
{
    private readonly string _connectionString;

    public ContentDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ContentDbContextFactory() : this(@"Host=localhost;Database=Sample;Username=postgres;Password=postgres")
    {

    }

    public ContentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContentDbContext>();
        _ = optionsBuilder.UseNpgsql(_connectionString, x => x.MigrationsAssembly(GetType().Assembly.FullName));

        return new ContentDbContext(optionsBuilder.Options);
    }
}
