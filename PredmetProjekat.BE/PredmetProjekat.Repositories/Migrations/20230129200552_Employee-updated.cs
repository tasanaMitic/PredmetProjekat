using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Employeeupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagedByUsername",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ManagedByUsername",
                table: "Accounts",
                column: "ManagedByUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ManagedByUsername",
                table: "Accounts",
                column: "ManagedByUsername",
                principalTable: "Accounts",
                principalColumn: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ManagedByUsername",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ManagedByUsername",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ManagedByUsername",
                table: "Accounts");
        }
    }
}
