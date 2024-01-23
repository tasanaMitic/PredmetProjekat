using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class attributeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "Attributes");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductTypes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes");


            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductTypes");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
