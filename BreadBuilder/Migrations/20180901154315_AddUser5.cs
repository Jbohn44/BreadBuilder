using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class AddUser5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Breads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Breads",
                nullable: false,
                defaultValue: 0);
        }
    }
}
