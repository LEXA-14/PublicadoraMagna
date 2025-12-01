using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "RolSistema",
                table: "Usuario",
                newName: "NombreCompleto");

            migrationBuilder.AddColumn<int>(
                name: "PeriodistaId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PeriodistaId",
                table: "Usuario",
                column: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Periodistas_PeriodistaId",
                table: "Usuario",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Periodistas_PeriodistaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PeriodistaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "PeriodistaId",
                table: "Usuario");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "Usuario",
                newName: "RolSistema");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
