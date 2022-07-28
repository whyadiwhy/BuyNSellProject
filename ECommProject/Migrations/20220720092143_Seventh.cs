using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommProject.Migrations
{
    public partial class Seventh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_product_productid",
                table: "cart");

            migrationBuilder.DropIndex(
                name: "IX_cart_productid",
                table: "cart");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "cart",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
