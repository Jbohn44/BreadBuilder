using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Breads",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Breads_UserID",
                table: "Breads",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Breads_Users_UserID",
                table: "Breads",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breads_Users_UserID",
                table: "Breads");

            migrationBuilder.DropIndex(
                name: "IX_Breads_UserID",
                table: "Breads");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Breads");
        }
    }
}
