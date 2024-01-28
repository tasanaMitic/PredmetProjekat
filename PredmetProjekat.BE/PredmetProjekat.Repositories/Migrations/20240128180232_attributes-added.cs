using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class attributesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "AttributeValue",
                columns: table => new
                {
                    AttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.AttributeValueId);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });


            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_AttributeId",
                table: "AttributeValue",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_ProductId",
                table: "AttributeValue",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValue");
        }
    }
}
