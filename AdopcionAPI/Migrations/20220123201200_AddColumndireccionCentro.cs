using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class AddColumndireccionCentro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Centros",
                type: "character varying(100)",
                maxLength: 100,
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
