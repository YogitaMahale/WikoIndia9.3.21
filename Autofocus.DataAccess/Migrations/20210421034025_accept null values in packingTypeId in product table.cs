using Microsoft.EntityFrameworkCore.Migrations;

namespace Autofocus.DataAccess.Migrations
{
    public partial class acceptnullvaluesinpackingTypeIdinproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_packingType_packingTypeId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "packingTypeId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_packingType_packingTypeId",
                table: "Product",
                column: "packingTypeId",
                principalTable: "packingType",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_packingType_packingTypeId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "packingTypeId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_packingType_packingTypeId",
                table: "Product",
                column: "packingTypeId",
                principalTable: "packingType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
