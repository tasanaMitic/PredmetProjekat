using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Adminupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "account_type",
                table: "Accounts",
                newName: "AccountType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "Accounts",
                newName: "account_type");
        }
    }
}
