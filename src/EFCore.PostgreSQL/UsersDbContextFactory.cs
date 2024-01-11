using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;

namespace EFCore.PostgreSQL;

internal class UsersDbContextFactory(string connectionString) : IDesignTimeDbContextFactory<UsersDbContext>
{
    private readonly string _connectionString = connectionString;

    public UsersDbContextFactory() : this(@"Host=localhost;Database=Sample;Username=postgres;Password=postgres")
    {

    }

    public UsersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        _ = optionsBuilder.UseNpgsql(_connectionString, x => x.MigrationsAssembly(GetType().Assembly.FullName));

        return new UsersDbContext(optionsBuilder.Options);
    }
}
