using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Employeeupdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ManagedByUsername",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ManagedByUsername",
                table: "Accounts",
                newName: "ManagerUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ManagedByUsername",
                table: "Accounts",
                newName: "IX_Accounts_ManagerUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts",
                column: "ManagerUsername",
                principalTable: "Accounts",
                principalColumn: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ManagerUsername",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ManagerUsername",
                table: "Accounts",
                newName: "ManagedByUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ManagerUsername",
                table: "Accounts",
                newName: "IX_Accounts_ManagedByUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ManagedByUsername",
                table: "Accounts",
                column: "ManagedByUsername",
                principalTable: "Accounts",
                principalColumn: "Username");
        }
    }
}
