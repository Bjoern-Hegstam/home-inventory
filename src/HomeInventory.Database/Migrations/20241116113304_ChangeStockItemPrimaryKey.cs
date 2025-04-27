using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeInventory.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStockItemPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StockItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem",
                column: "StockItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StockItem",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockItem",
                table: "StockItem",
                column: "Id");
        }
    }
}
