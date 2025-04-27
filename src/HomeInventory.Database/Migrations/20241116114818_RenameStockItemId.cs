using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeInventory.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameStockItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockItemId",
                table: "StockItem",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_StockItemId",
                table: "StockItem",
                newName: "IX_StockItem_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StockItem",
                newName: "StockItemId");

            migrationBuilder.RenameIndex(
                name: "IX_StockItem_Id",
                table: "StockItem",
                newName: "IX_StockItem_StockItemId");
        }
    }
}
