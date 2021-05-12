using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDelivery.Data.Migrations
{
    public partial class addrestauranttoshoppingcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "ShoppingCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_RestaurantId",
                table: "ShoppingCarts",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Restaurant_RestaurantId",
                table: "ShoppingCarts",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Restaurant_RestaurantId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_RestaurantId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "ShoppingCarts");
        }
    }
}
