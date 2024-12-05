using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class fixthethingsibroke3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentMethodId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ShippingAddressId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    ExpirationMonth = table.Column<byte>(nullable: false),
                    ExpirationYear = table.Column<short>(nullable: false),
                    PIN = table.Column<string>(nullable: true),
                    BillingAddressId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardPaymentMethod_Address_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingAddressId",
                table: "Order",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CardPaymentMethod_BillingAddressId",
                table: "CardPaymentMethod",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_CardPaymentMethod_PaymentMethodId",
                table: "Order",
                column: "PaymentMethodId",
                principalTable: "CardPaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_ShippingAddressId",
                table: "Order",
                column: "ShippingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_CardPaymentMethod_PaymentMethodId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_ShippingAddressId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "CardPaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_Order_PaymentMethodId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ShippingAddressId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "Order");
        }
    }
}
