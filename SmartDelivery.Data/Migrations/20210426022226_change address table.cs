using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDelivery.Data.Migrations
{
    public partial class changeaddresstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Address",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Address");
        }
    }
}
