using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class OnmodelCreatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Categorias_CategoriaId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Instituciones_InstitucionId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Periodistas_PeriodistaId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosPeriodistas_Articulos_ArticuloId",
                table: "DetallesPagosPeriodistas");

            migrationBuilder.DropForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_PagosPeriodistas_Periodistas_PeriodistaId",
                table: "PagosPeriodistas");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ServiciosPromocionales",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Periodistas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "EsActivo",
                table: "Periodistas",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PagosPeriodistas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PagosInstitucion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Instituciones",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rnc",
                table: "Instituciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Instituciones",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAdmin",
                table: "Instituciones",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CorreoContacto",
                table: "Instituciones",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InstitucionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodistaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Articulos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InstitucionId",
                table: "AspNetUsers",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PeriodistaId",
                table: "AspNetUsers",
                column: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Categorias_CategoriaId",
                table: "Articulos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Instituciones_InstitucionId",
                table: "Articulos",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Periodistas_PeriodistaId",
                table: "Articulos",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional",
                column: "ServicioPromocionalId",
                principalTable: "ServiciosPromocionales",
                principalColumn: "ServicioPromocionalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Instituciones_InstitucionId",
                table: "AspNetUsers",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Periodistas_PeriodistaId",
                table: "AspNetUsers",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosPeriodistas_Articulos_ArticuloId",
                table: "DetallesPagosPeriodistas",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PagosPeriodistas_Periodistas_PeriodistaId",
                table: "PagosPeriodistas",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Categorias_CategoriaId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Instituciones_InstitucionId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Periodistas_PeriodistaId",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Instituciones_InstitucionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Periodistas_PeriodistaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPagosPeriodistas_Articulos_ArticuloId",
                table: "DetallesPagosPeriodistas");

            migrationBuilder.DropForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion");

            migrationBuilder.DropForeignKey(
                name: "FK_PagosPeriodistas_Periodistas_PeriodistaId",
                table: "PagosPeriodistas");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InstitucionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PeriodistaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstitucionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PeriodistaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ServiciosPromocionales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Periodistas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<bool>(
                name: "EsActivo",
                table: "Periodistas",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PagosPeriodistas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "PagosInstitucion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Rnc",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAdmin",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "CorreoContacto",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstitucionId = table.Column<int>(type: "int", nullable: true),
                    PeriodistaId = table.Column<int>(type: "int", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "InstitucionId");
                    table.ForeignKey(
                        name: "FK_Usuario_Periodistas_PeriodistaId",
                        column: x => x.PeriodistaId,
                        principalTable: "Periodistas",
                        principalColumn: "PeriodistaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_InstitucionId",
                table: "Usuario",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PeriodistaId",
                table: "Usuario",
                column: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Categorias_CategoriaId",
                table: "Articulos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Instituciones_InstitucionId",
                table: "Articulos",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Periodistas_PeriodistaId",
                table: "Articulos",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticuloServicioPromocional_ServiciosPromocionales_ServicioPromocionalId",
                table: "ArticuloServicioPromocional",
                column: "ServicioPromocionalId",
                principalTable: "ServiciosPromocionales",
                principalColumn: "ServicioPromocionalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosInstitucion_Articulos_ArticuloId",
                table: "DetallesPagosInstitucion",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPagosPeriodistas_Articulos_ArticuloId",
                table: "DetallesPagosPeriodistas",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "ArticuloId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PagosInstitucion_Instituciones_InstitucionId",
                table: "PagosInstitucion",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "InstitucionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PagosPeriodistas_Periodistas_PeriodistaId",
                table: "PagosPeriodistas",
                column: "PeriodistaId",
                principalTable: "Periodistas",
                principalColumn: "PeriodistaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
