using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class receiptupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldProduct_Products_ProductId",
                table: "SoldProduct");

            migrationBuilder.DropIndex(
                name: "IX_SoldProduct_ProductId",
                table: "SoldProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SoldProduct_ProductId",
                table: "SoldProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProduct_Products_ProductId",
                table: "SoldProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
