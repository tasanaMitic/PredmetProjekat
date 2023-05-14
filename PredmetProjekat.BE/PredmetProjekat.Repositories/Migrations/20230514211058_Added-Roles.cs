using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "AspNetUsers",
                newName: "Discriminator");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20a38a75-9faa-4be7-8dc4-c8cc7eb988b0", "d9aa21c8-c9d7-42a6-a9c2-e3dfedeb92e2", "Employee", "EMPLOYEE" },
                    { "2680ef84-1770-4378-9ccd-aa67b69321a0", "cd595fb3-b04f-47b3-923e-ad937705cda6", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20a38a75-9faa-4be7-8dc4-c8cc7eb988b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2680ef84-1770-4378-9ccd-aa67b69321a0");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "AspNetUsers",
                newName: "AccountType");
        }
    }
}
