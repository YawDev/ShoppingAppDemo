using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAppDemo.Migrations
{
    public partial class sessioncart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CartSessionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartSessionId",
                table: "AspNetUsers");
        }
    }
}
