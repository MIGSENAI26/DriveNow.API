using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNow.API.Migrations
{
    /// <inheritdoc />
    public partial class Upload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoUrlVeiculo",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrlCliente",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUrlVeiculo",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "FotoUrlCliente",
                table: "Clientes");
        }
    }
}
