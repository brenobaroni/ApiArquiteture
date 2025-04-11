using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_items_sales_id",
                schema: "shop",
                table: "sales_items");

            migrationBuilder.CreateIndex(
                name: "IX_sales_items_sale_id",
                schema: "shop",
                table: "sales_items",
                column: "sale_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_items_sales_sale_id",
                schema: "shop",
                table: "sales_items",
                column: "sale_id",
                principalSchema: "shop",
                principalTable: "sales",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_items_sales_sale_id",
                schema: "shop",
                table: "sales_items");

            migrationBuilder.DropIndex(
                name: "IX_sales_items_sale_id",
                schema: "shop",
                table: "sales_items");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_items_sales_id",
                schema: "shop",
                table: "sales_items",
                column: "id",
                principalSchema: "shop",
                principalTable: "sales",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
