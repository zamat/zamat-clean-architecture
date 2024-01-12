namespace Zamat.Common.EntityFrameworkCore;

public class AssemblyMigrationContext
{
    public string PostgreSQL { get; set; } = string.Empty;
    public string MSSQL { get; set; } = string.Empty;
    public string Oracle { get; set; } = string.Empty;
    public string MigrationsHistoryTable { get; set; } = string.Empty;
    public string? Schema { get; set; } = null;
}
