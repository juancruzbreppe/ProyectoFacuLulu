using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FichaMedica.Migrations
{
    /// <inheritdoc />
    public partial class CambiosModelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsAlergico",
                table: "Fichas");

            migrationBuilder.RenameColumn(
                name: "TelefonoContactoEmergencia",
                table: "Fichas",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "NombreContactoEmergencia",
                table: "Fichas",
                newName: "MedicacionRegularDetalle");

            migrationBuilder.RenameColumn(
                name: "MedicacionActual",
                table: "Fichas",
                newName: "MedicacionRegular");

            migrationBuilder.RenameColumn(
                name: "EsDiabetico",
                table: "Fichas",
                newName: "AceptaTerminos");

            migrationBuilder.RenameColumn(
                name: "Enfermedades",
                table: "Fichas",
                newName: "InformacionAdicional");

            migrationBuilder.RenameColumn(
                name: "Alergias",
                table: "Fichas",
                newName: "FumaDetalle");

            migrationBuilder.AddColumn<string>(
                name: "Alergia",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AlergiasDetalle",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AlturaCm",
                table: "Fichas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CirugiaReciente",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CirugiaRecienteDetalle",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CondicionCronica",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CondicionesCronicasDetalle",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Droga",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DrogaDetalle",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Fuma",
                table: "Fichas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "PesoKg",
                table: "Fichas",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alergia",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "AlergiasDetalle",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "AlturaCm",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "CirugiaReciente",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "CirugiaRecienteDetalle",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "CondicionCronica",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "CondicionesCronicasDetalle",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "Droga",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "DrogaDetalle",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "Fuma",
                table: "Fichas");

            migrationBuilder.DropColumn(
                name: "PesoKg",
                table: "Fichas");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Fichas",
                newName: "TelefonoContactoEmergencia");

            migrationBuilder.RenameColumn(
                name: "MedicacionRegularDetalle",
                table: "Fichas",
                newName: "NombreContactoEmergencia");

            migrationBuilder.RenameColumn(
                name: "MedicacionRegular",
                table: "Fichas",
                newName: "MedicacionActual");

            migrationBuilder.RenameColumn(
                name: "InformacionAdicional",
                table: "Fichas",
                newName: "Enfermedades");

            migrationBuilder.RenameColumn(
                name: "FumaDetalle",
                table: "Fichas",
                newName: "Alergias");

            migrationBuilder.RenameColumn(
                name: "AceptaTerminos",
                table: "Fichas",
                newName: "EsDiabetico");

            migrationBuilder.AddColumn<bool>(
                name: "EsAlergico",
                table: "Fichas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
