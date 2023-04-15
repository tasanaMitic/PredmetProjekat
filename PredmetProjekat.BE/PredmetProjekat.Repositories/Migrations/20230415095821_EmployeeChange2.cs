using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeChange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts",
                column: "ManagerUsername",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts",
                column: "ManagerUsername",
                principalTable: "Accounts",
                principalColumn: "Username");
        }
    }
}
