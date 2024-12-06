using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class paymentmethodsmodelsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "CardPaymentMethod");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CardPaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVC",
                table: "CardPaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CardPaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CardPaymentMethod",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardPaymentMethod_ApplicationUserId",
                table: "CardPaymentMethod",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardPaymentMethod_AspNetUsers_ApplicationUserId",
                table: "CardPaymentMethod",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardPaymentMethod_AspNetUsers_ApplicationUserId",
                table: "CardPaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_CardPaymentMethod_ApplicationUserId",
                table: "CardPaymentMethod");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CardPaymentMethod");

            migrationBuilder.DropColumn(
                name: "CVC",
                table: "CardPaymentMethod");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CardPaymentMethod");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CardPaymentMethod");

            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "CardPaymentMethod",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
