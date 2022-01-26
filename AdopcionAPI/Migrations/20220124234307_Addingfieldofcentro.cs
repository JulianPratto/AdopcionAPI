using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class Addingfieldofcentro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Centros",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Centros");
        }
    }
}
