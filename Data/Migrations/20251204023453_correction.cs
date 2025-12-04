using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosPromocionales_EncargoArticulos_EncargoArticuloId",
                table: "ServiciosPromocionales");

            migrationBuilder.DropIndex(
                name: "IX_ServiciosPromocionales_EncargoArticuloId",
                table: "ServiciosPromocionales");

            migrationBuilder.DropColumn(
                name: "EncargoArticuloId",
                table: "ServiciosPromocionales");

            migrationBuilder.AddColumn<int>(
                name: "EncargoArticuloId",
                table: "ArticuloServicioPromocional",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloServicioPromocional_EncargoArticuloId",
                table: "ArticuloServicioPromocional",
                column: "EncargoArticuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloServicioPromocional_EncargoArticulos_EncargoArticuloId",
                table: "ArticuloServicioPromocional",
                column: "EncargoArticuloId",
                principalTable: "EncargoArticulos",
                principalColumn: "EncargoArticuloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloServicioPromocional_EncargoArticulos_EncargoArticuloId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropIndex(
                name: "IX_ArticuloServicioPromocional_EncargoArticuloId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropColumn(
                name: "EncargoArticuloId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.AddColumn<int>(
                name: "EncargoArticuloId",
                table: "ServiciosPromocionales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosPromocionales_EncargoArticuloId",
                table: "ServiciosPromocionales",
                column: "EncargoArticuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosPromocionales_EncargoArticulos_EncargoArticuloId",
                table: "ServiciosPromocionales",
                column: "EncargoArticuloId",
                principalTable: "EncargoArticulos",
                principalColumn: "EncargoArticuloId");
        }
    }
}
