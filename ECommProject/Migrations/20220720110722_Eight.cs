using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommProject.Migrations
{
    public partial class Eight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_category_categoryId",
                table: "inventory");

            migrationBuilder.DropIndex(
                name: "IX_inventory_categoryId",
                table: "inventory");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "inventory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_categoryId",
                table: "inventory",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_category_categoryId",
                table: "inventory",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
