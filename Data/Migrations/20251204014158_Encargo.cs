using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class Encargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncargoArticuloId",
                table: "ServiciosPromocionales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EncargoArticulos",
                columns: table => new
                {
                    EncargoArticuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitucionId = table.Column<int>(type: "int", nullable: false),
                    PeriodistaId = table.Column<int>(type: "int", nullable: false),
                    TituloSugerido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionEncargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRespuestaPeriodista = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEnvioInstitucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRespuestaInstitucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComentarioPeriodista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComentarioInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticuloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncargoArticulos", x => x.EncargoArticuloId);
                    table.ForeignKey(
                        name: "FK_EncargoArticulos_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "ArticuloId");
                    table.ForeignKey(
                        name: "FK_EncargoArticulos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncargoArticulos_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "InstitucionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncargoArticulos_Periodistas_PeriodistaId",
                        column: x => x.PeriodistaId,
                        principalTable: "Periodistas",
                        principalColumn: "PeriodistaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosPromocionales_EncargoArticuloId",
                table: "ServiciosPromocionales",
                column: "EncargoArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_EncargoArticulos_ArticuloId",
                table: "EncargoArticulos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_EncargoArticulos_CategoriaId",
                table: "EncargoArticulos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncargoArticulos_InstitucionId",
                table: "EncargoArticulos",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_EncargoArticulos_PeriodistaId",
                table: "EncargoArticulos",
                column: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosPromocionales_EncargoArticulos_EncargoArticuloId",
                table: "ServiciosPromocionales",
                column: "EncargoArticuloId",
                principalTable: "EncargoArticulos",
                principalColumn: "EncargoArticuloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosPromocionales_EncargoArticulos_EncargoArticuloId",
                table: "ServiciosPromocionales");

            migrationBuilder.DropTable(
                name: "EncargoArticulos");

            migrationBuilder.DropIndex(
                name: "IX_ServiciosPromocionales_EncargoArticuloId",
                table: "ServiciosPromocionales");

            migrationBuilder.DropColumn(
                name: "EncargoArticuloId",
                table: "ServiciosPromocionales");
        }
    }
}
