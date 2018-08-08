using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class AddRecipeItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Breads_BreadID",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_BreadID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "BreadID",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "RecipeItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecipeIngredientID = table.Column<int>(nullable: true),
                    RecipeMeasurementID = table.Column<int>(nullable: true),
                    BreadID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Breads_BreadID",
                        column: x => x.BreadID,
                        principalTable: "Breads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Ingredients_RecipeIngredientID",
                        column: x => x.RecipeIngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Measurements_RecipeMeasurementID",
                        column: x => x.RecipeMeasurementID,
                        principalTable: "Measurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_BreadID",
                table: "RecipeItems",
                column: "BreadID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeIngredientID",
                table: "RecipeItems",
                column: "RecipeIngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeMeasurementID",
                table: "RecipeItems",
                column: "RecipeMeasurementID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.AddColumn<int>(
                name: "BreadID",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_BreadID",
                table: "Ingredients",
                column: "BreadID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Breads_BreadID",
                table: "Ingredients",
                column: "BreadID",
                principalTable: "Breads",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
