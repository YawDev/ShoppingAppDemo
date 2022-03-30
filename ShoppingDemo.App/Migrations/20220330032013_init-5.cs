using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppDemo.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_Items_ItemListingId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemListingId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ItemListingId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_ItemListingId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "ItemListingId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ItemListingId",
                table: "orderItems");

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "ShoppingCartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "orderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "orderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "orderItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemListingId",
                table: "ShoppingCartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemListingId",
                table: "orderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ItemListingId",
                table: "ShoppingCartItems",
                column: "ItemListingId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_ItemListingId",
                table: "orderItems",
                column: "ItemListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_Items_ItemListingId",
                table: "orderItems",
                column: "ItemListingId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemListingId",
                table: "ShoppingCartItems",
                column: "ItemListingId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
