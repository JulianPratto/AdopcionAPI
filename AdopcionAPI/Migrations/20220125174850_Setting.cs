using Microsoft.EntityFrameworkCore.Migrations;

namespace AdopcionAPI.Migrations
{
    public partial class Setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Centros_CentroId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_CentroId",
                table: "Mascotas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_CentroId",
                table: "Mascotas",
                column: "CentroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Centros_CentroId",
                table: "Mascotas",
                column: "CentroId",
                principalTable: "Centros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
