using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicadoraMagna.Migrations
{
    /// <inheritdoc />
    public partial class balances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAPagar",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAPagar",
                table: "Articulos");
        }
    }
}
