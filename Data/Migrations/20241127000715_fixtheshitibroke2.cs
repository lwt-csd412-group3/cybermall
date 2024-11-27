using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class fixtheshitibroke2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "ItemListings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemListings_SellerId",
                table: "ItemListings",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemListings_AspNetUsers_SellerId",
                table: "ItemListings");

            migrationBuilder.DropIndex(
                name: "IX_ItemListings_SellerId",
                table: "ItemListings");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "ItemListings");
        }
    }
}
