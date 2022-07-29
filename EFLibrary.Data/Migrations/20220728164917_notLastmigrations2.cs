using Microsoft.EntityFrameworkCore.Migrations;

namespace EFLibrary.Data.Migrations
{
    public partial class notLastmigrations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Employee",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Employee");
        }
    }
}
