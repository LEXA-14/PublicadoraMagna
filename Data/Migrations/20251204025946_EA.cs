using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class EA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EncargoServicioPromocional",
                columns: table => new
                {
                    EncargoServicioPromocionalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncargoArticuloId = table.Column<int>(type: "int", nullable: false),
                    ServicioPromocionalId = table.Column<int>(type: "int", nullable: false),
                    PrecioAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncargoServicioPromocional", x => x.EncargoServicioPromocionalId);
                    table.ForeignKey(
                        name: "FK_EncargoServicioPromocional_EncargoArticulos_EncargoArticuloId",
                        column: x => x.EncargoArticuloId,
                        principalTable: "EncargoArticulos",
                        principalColumn: "EncargoArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncargoServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                        column: x => x.ServicioPromocionalId,
                        principalTable: "ServiciosPromocionales",
                        principalColumn: "ServicioPromocionalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncargoServicioPromocional_EncargoArticuloId",
                table: "EncargoServicioPromocional",
                column: "EncargoArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_EncargoServicioPromocional_ServicioPromocionalId",
                table: "EncargoServicioPromocional",
                column: "ServicioPromocionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncargoServicioPromocional");

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
    }
}
