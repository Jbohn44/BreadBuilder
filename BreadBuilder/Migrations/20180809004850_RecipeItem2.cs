using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class RecipeItem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeItems_Breads_BreadID",
                table: "RecipeItems");

            migrationBuilder.DropIndex(
                name: "IX_RecipeItems_BreadID",
                table: "RecipeItems");

            migrationBuilder.DropColumn(
                name: "BreadID",
                table: "RecipeItems");

            migrationBuilder.RenameColumn(
                name: "MeasurementValue",
                table: "Measurements",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Measurements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecipeItemID",
                table: "Breads",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Breads_RecipeItemID",
                table: "Breads",
                column: "RecipeItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads",
                column: "RecipeItemID",
                principalTable: "RecipeItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breads_RecipeItems_RecipeItemID",
                table: "Breads");

            migrationBuilder.DropIndex(
                name: "IX_Breads_RecipeItemID",
                table: "Breads");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "RecipeItemID",
                table: "Breads");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Measurements",
                newName: "MeasurementValue");

            migrationBuilder.AddColumn<int>(
                name: "BreadID",
                table: "RecipeItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_BreadID",
                table: "RecipeItems",
                column: "BreadID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeItems_Breads_BreadID",
                table: "RecipeItems",
                column: "BreadID",
                principalTable: "Breads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
