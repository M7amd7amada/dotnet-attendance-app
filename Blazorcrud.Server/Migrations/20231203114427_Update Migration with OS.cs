using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazorcrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationwithOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Browser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "Users");
        }
    }
}
