using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class novaatt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativa",
                table: "Vagas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativa",
                table: "Vagas");
        }
    }
}
