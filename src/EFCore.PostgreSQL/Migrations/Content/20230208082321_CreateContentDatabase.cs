using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.PostgreSQL.Migrations.Content;

/// <inheritdoc />
public partial class CreateContentDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "Articles",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<string>(type: "text", nullable: false),
                Title = table.Column<string>(type: "text", nullable: false),
                Text = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Articles", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Articles",
            schema: "dbo");
    }
}
