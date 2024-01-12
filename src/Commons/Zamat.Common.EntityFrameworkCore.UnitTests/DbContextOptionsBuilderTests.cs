using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Xunit;

namespace AUMS.Common.EntityFrameworkCore.UnitTests;

public class DbContextOptionsBuilderTests
{
    [Fact]
    public void RemoveNpgsqlStringPart()
    {
        string connectionstring = "localhost;user=user";

        var builder = new DbContextOptionsBuilder();
        builder.UseAutoDbProvider(Consts.PostgreSQLPrefix + connectionstring);

        var options = builder.Options;

        var connectionStringParsed = ((NpgsqlOptionsExtension)options.Extensions.First()).ConnectionString!;

        Assert.Equal(connectionstring, connectionStringParsed);
    }
}
