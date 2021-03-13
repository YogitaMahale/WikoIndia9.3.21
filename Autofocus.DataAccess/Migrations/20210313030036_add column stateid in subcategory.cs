using Microsoft.EntityFrameworkCore.Migrations;

namespace Autofocus.DataAccess.Migrations
{
    public partial class addcolumnstateidinsubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "stateid",
                table: "Subcategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stateid",
                table: "Subcategory");
        }
    }
}
