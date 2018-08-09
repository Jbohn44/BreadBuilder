using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class RecipeItem3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeItemID",
                table: "Breads",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads",
                column: "RecipeItemID",
                principalTable: "RecipeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeItemID",
                table: "Breads",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads",
                column: "RecipeItemID",
                principalTable: "RecipeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
