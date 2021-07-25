using Microsoft.EntityFrameworkCore.Migrations;

namespace Autofocus.DataAccess.Migrations
{
    public partial class addisNegotiablecolumninproductdetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNegotiable",
                table: "ProductDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNegotiable",
                table: "ProductDetails");
        }
    }
}
