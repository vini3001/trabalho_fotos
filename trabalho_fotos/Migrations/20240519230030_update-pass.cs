using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trabalho_fotos.Migrations
{
    /// <inheritdoc />
    public partial class updatepass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clientes");
        }
    }
}
