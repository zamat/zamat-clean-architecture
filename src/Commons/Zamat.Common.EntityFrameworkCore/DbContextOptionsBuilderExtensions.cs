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

    internal static DbContextOptionsBuilder ConfigureDbProvider(this DbContextOptionsBuilder optionsBuilder, string connectionString, AssemblyMigrationContext assemblyMigrationContext)
    {
        if (connectionString.StartsWith(Consts.PostgreSQLPrefix))
        {
            optionsBuilder.UseNpgsql(connectionString[Consts.PostgreSQLPrefix.Length..], (opt) =>
            {
                if (!string.IsNullOrEmpty(assemblyMigrationContext.PostgreSQL))
                {
                    opt.MigrationsAssembly(assemblyMigrationContext.PostgreSQL);
                }

                if (!string.IsNullOrEmpty(assemblyMigrationContext.MigrationsHistoryTable))
                {
                    opt.MigrationsHistoryTable(assemblyMigrationContext.MigrationsHistoryTable, assemblyMigrationContext.Schema);
                }
            });
            return optionsBuilder;
        }

        if (connectionString.StartsWith(Consts.OraclePrefix))
        {
            optionsBuilder.UseOracle(connectionString[Consts.OraclePrefix.Length..], (opt) =>
            {
                if (!string.IsNullOrEmpty(assemblyMigrationContext.Oracle))
                {
                    opt.MigrationsAssembly(assemblyMigrationContext.Oracle);
                }

                if (!string.IsNullOrEmpty(assemblyMigrationContext.MigrationsHistoryTable))
                {
                    opt.MigrationsHistoryTable(assemblyMigrationContext.MigrationsHistoryTable, assemblyMigrationContext.Schema);
                }
            });

            return optionsBuilder;
        }

        if (connectionString.StartsWith(Consts.MSSQLPrefix))
        {
            optionsBuilder.UseSqlServer(connectionString[Consts.MSSQLPrefix.Length..], (opt) =>
            {
                if (!string.IsNullOrEmpty(assemblyMigrationContext.MSSQL))
                {
                    opt.MigrationsAssembly(assemblyMigrationContext.MSSQL);
                }

                if (!string.IsNullOrEmpty(assemblyMigrationContext.MigrationsHistoryTable))
                {
                    opt.MigrationsHistoryTable(assemblyMigrationContext.MigrationsHistoryTable, assemblyMigrationContext.Schema);
                }
            });

            return optionsBuilder;
        }

        throw new Exception("Unsupported dbprovider. Connection string should contains valid prefix (e.g. postgresql:// or oracle://)");
    }
}
