using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class comentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComentarioRechazo",
                table: "EncargoArticulos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComentarioRechazo",
                table: "EncargoArticulos");
        }
    }
}
