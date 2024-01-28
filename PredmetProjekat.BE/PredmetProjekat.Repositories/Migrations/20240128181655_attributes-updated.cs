using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PredmetProjekat.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class attributesupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValue_Attributes_AttributeId",
                table: "AttributeValue");


            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "AttributeValue",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValue_AttributeId",
                table: "AttributeValue",
                newName: "IX_AttributeValue_ProductAttributeId");


            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValue_Attributes_ProductAttributeId",
                table: "AttributeValue",
                column: "ProductAttributeId",
                principalTable: "Attributes",
                principalColumn: "AttributeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValue_Attributes_ProductAttributeId",
                table: "AttributeValue");


            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "AttributeValue",
                newName: "AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeValue_ProductAttributeId",
                table: "AttributeValue",
                newName: "IX_AttributeValue_AttributeId");


            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValue_Attributes_AttributeId",
                table: "AttributeValue",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "AttributeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
