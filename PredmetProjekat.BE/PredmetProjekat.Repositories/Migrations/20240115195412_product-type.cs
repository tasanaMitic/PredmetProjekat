using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class producttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoldProduct_Products_ProductId",
                table: "SoldProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProduct_Receipts_ReceiptId",
                table: "SoldProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldProduct",
                table: "SoldProduct");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "SoldProduct",
                newName: "SoldProducts");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProduct_ReceiptId",
                table: "SoldProducts",
                newName: "IX_SoldProducts_ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProduct_ProductId",
                table: "SoldProducts",
                newName: "IX_SoldProducts_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldProducts",
                table: "SoldProducts",
                column: "SoldProductId");

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_Attributes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "ProductTypeId");
                });

   

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ProductTypeId",
                table: "Attributes",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Products_ProductId",
                table: "SoldProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProducts_Receipts_ReceiptId",
                table: "SoldProducts",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Products_ProductId",
                table: "SoldProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoldProducts_Receipts_ReceiptId",
                table: "SoldProducts");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoldProducts",
                table: "SoldProducts");

       

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "SoldProducts",
                newName: "SoldProduct");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProducts_ReceiptId",
                table: "SoldProduct",
                newName: "IX_SoldProduct_ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_SoldProducts_ProductId",
                table: "SoldProduct",
                newName: "IX_SoldProduct_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoldProduct",
                table: "SoldProduct",
                column: "SoldProductId");

       

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProduct_Products_ProductId",
                table: "SoldProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoldProduct_Receipts_ReceiptId",
                table: "SoldProduct",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "ReceiptId");
        }
    }
}
