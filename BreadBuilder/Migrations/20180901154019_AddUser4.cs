using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class AddUser4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breads_Users_UserID",
                table: "Breads");

            migrationBuilder.DropIndex(
                name: "IX_Breads_UserID",
                table: "Breads");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Breads",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Breads",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
