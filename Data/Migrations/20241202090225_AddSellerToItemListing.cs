using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class AddSellerToItemListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
