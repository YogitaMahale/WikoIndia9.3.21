using Microsoft.EntityFrameworkCore.Migrations;

namespace Autofocus.DataAccess.Migrations
{
    public partial class addpackingeTypeIdinproductdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "packingeTypeId",
                table: "ProductDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "packingeTypeId",
                table: "ProductDetails");
        }
    }
}
