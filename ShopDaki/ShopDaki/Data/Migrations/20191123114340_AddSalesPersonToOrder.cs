using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopDaki.Data.Migrations
{
    public partial class AddSalesPersonToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalesPersonId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_SalesPersonId",
                table: "Order",
                column: "SalesPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_SalesPersonId",
                table: "Order",
                column: "SalesPersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_SalesPersonId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_SalesPersonId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "Order");
        }
    }
}
