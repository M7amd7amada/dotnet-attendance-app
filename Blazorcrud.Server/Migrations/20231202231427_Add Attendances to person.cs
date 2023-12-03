using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazorcrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendancestoperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Attendances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_PersonId",
                table: "Attendances",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_PersonId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Attendances");
        }
    }
}
