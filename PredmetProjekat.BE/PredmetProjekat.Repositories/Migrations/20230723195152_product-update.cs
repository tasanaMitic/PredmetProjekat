using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class productupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20a38a75-9faa-4be7-8dc4-c8cc7eb988b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2680ef84-1770-4378-9ccd-aa67b69321a0");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36fa7a6d-c012-464f-a83c-067d46da1d16", "9cec45e8-65a2-4ef3-8c36-95043a9b3a9c", "Employee", "EMPLOYEE" },
                    { "b45e24f0-08b7-4fb3-b5e2-da00c18accf3", "468bb384-cc8f-4fb3-a3ac-84966c2e2f66", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36fa7a6d-c012-464f-a83c-067d46da1d16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b45e24f0-08b7-4fb3-b5e2-da00c18accf3");

            migrationBuilder.DropColumn(
                name: "IsInStock",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20a38a75-9faa-4be7-8dc4-c8cc7eb988b0", "d9aa21c8-c9d7-42a6-a9c2-e3dfedeb92e2", "Employee", "EMPLOYEE" },
                    { "2680ef84-1770-4378-9ccd-aa67b69321a0", "cd595fb3-b04f-47b3-923e-ad937705cda6", "Admin", "ADMIN" }
                });
        }
    }
}
