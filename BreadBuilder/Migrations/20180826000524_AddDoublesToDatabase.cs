using Microsoft.EntityFrameworkCore.Migrations;

namespace BreadBuilder.Migrations
{
    public partial class AddDoublesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Measurements",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Measurements",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
