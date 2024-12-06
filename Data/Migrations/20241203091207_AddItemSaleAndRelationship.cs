using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class AddItemSaleAndRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemSale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemListingId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSale_ItemListings_ItemListingId",
                        column: x => x.ItemListingId,
                        principalTable: "ItemListings",
                        principalColumn: "ItemListingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSale_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSale_ItemListingId",
                table: "ItemSale",
                column: "ItemListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSale_UserId",
                table: "ItemSale",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSale");
        }
    }
}
