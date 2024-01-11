using Microsoft.EntityFrameworkCore;

namespace Zamat.Common.EntityFrameworkCore;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseAutoDbProvider(this DbContextOptionsBuilder optionsBuilder, string connectionString, Action<AssemblyMigrationContext> configure)
    {
        var ctx = new AssemblyMigrationContext();
        configure(ctx);
        optionsBuilder.ConfigureDbProvider(connectionString, ctx);
        return optionsBuilder;
    }

    public static DbContextOptionsBuilder UseAutoDbProvider(this DbContextOptionsBuilder optionsBuilder, string connectionString)
    {
        optionsBuilder.ConfigureDbProvider(connectionString, new AssemblyMigrationContext());
        return optionsBuilder;
    }

    private static DbContextOptionsBuilder ConfigureDbProvider(this DbContextOptionsBuilder optionsBuilder, string connectionString, AssemblyMigrationContext assemblyMigrationContext)
    {
        if (connectionString.StartsWith(Consts.PostgreSQLPrefix))
        {
            optionsBuilder.UseNpgsql(connectionString[Consts.PostgreSQLPrefix.Length..], (opt) =>
            {
                if (!string.IsNullOrEmpty(assemblyMigrationContext.PostgreSQL))
                {
                    opt.MigrationsAssembly(assemblyMigrationContext.PostgreSQL);
                }
            });
            return optionsBuilder;
        }

        optionsBuilder.UseSqlServer(connectionString, (opt) =>
        {
            if (!string.IsNullOrEmpty(assemblyMigrationContext.SqlServer))
            {
                opt.MigrationsAssembly(assemblyMigrationContext.SqlServer);
            }
        });

        return optionsBuilder;
    }
}