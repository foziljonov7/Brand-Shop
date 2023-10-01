using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrandUzApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 2, 12, 21, 2, 107, DateTimeKind.Utc).AddTicks(40),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 1, 10, 51, 15, 90, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.CreateTable(
                name: "ProductProduct",
                columns: table => new
                {
                    ItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProduct", x => new { x.ItemsId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductProduct_Products_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProduct_ProductId",
                table: "ProductProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProduct");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 1, 10, 51, 15, 90, DateTimeKind.Utc).AddTicks(105),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 9, 2, 12, 21, 2, 107, DateTimeKind.Utc).AddTicks(40));
        }
    }
}
