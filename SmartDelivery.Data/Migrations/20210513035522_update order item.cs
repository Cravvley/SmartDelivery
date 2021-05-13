using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDelivery.Data.Migrations
{
    public partial class updateorderitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Geodata",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Geodata",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
