using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FichaMedica.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganCambiosEnElModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreContactoEmergencia",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContactoEmergencia",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreContactoEmergencia",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "TelefonoContactoEmergencia",
                table: "Fichas");
        }
    }
}
