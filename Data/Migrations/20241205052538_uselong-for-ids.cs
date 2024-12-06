using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberMall.Data.Migrations
{
    public partial class uselongforids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_PrimaryAddressAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSale_ItemListings_ItemListingId",
                table: "ItemSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemListings",
                table: "ItemListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSale",
                table: "ItemSale");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrimaryAddressAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemListingId",
                table: "ItemListings");

            migrationBuilder.DropColumn(
                name: "PrimaryAddressAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Order",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Order",
                newName: "TaxAmount");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingCost",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<long>(
                name: "ItemListingId",
                table: "ItemSale",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ItemSale",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "ItemSale",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ItemListings",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "PrimaryAddressId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Address",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemListings",
                table: "ItemListings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSale",
                table: "ItemSale",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSale_OrderId",
                table: "ItemSale",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrimaryAddressId",
                table: "AspNetUsers",
                column: "PrimaryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_PrimaryAddressId",
                table: "AspNetUsers",
                column: "PrimaryAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSale_ItemListings_ItemListingId",
                table: "ItemSale",
                column: "ItemListingId",
                principalTable: "ItemListings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSale_Order_OrderId",
                table: "ItemSale",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_PrimaryAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSale_ItemListings_ItemListingId",
                table: "ItemSale");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSale_Order_OrderId",
                table: "ItemSale");

            migrationBuilder.DropIndex(
                name: "IX_ItemSale_OrderId",
                table: "ItemSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemListings",
                table: "ItemListings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrimaryAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ItemSale");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemListings");

            migrationBuilder.DropColumn(
                name: "PrimaryAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Order",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TaxAmount",
                table: "Order",
                newName: "Discount");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ItemListingId",
                table: "ItemSale",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemSale",
                type: "int",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ItemListingId",
                table: "ItemListings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryAddressAddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemListings",
                table: "ItemListings",
                column: "ItemListingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSale",
                table: "ItemSale",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrimaryAddressAddressId",
                table: "AspNetUsers",
                column: "PrimaryAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_PrimaryAddressAddressId",
                table: "AspNetUsers",
                column: "PrimaryAddressAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSale_ItemListings_ItemListingId",
                table: "ItemSale",
                column: "ItemListingId",
                principalTable: "ItemListings",
                principalColumn: "ItemListingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
