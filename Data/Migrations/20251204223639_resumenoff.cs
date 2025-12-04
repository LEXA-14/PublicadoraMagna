using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class resumenoff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "EncargoServicioPromocional");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "EncargoServicioPromocional");

            migrationBuilder.AlterColumn<string>(
                name: "Resumen",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "EncargoServicioPromocional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "EncargoServicioPromocional",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Resumen",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
