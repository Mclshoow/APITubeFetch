using Microsoft.EntityFrameworkCore.Migrations;

namespace APITubefetch.Migrations
{
    public partial class nameemailtodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Todos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Todos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Todos");
        }
    }
}
