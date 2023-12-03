using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazorcrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPayroll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseSalary",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PayrollPeriod",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Attendances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PayrollPeriod",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Attendances",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_People_PersonId",
                table: "Attendances",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId");
        }
    }
}
