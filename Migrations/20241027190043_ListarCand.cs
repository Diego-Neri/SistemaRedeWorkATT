using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class ListarCand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CadastrarVagasModelId",
                table: "Estudantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurriculoId",
                table: "Estudantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estudantes_CadastrarVagasModelId",
                table: "Estudantes",
                column: "CadastrarVagasModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudantes_Vagas_CadastrarVagasModelId",
                table: "Estudantes",
                column: "CadastrarVagasModelId",
                principalTable: "Vagas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudantes_Vagas_CadastrarVagasModelId",
                table: "Estudantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudantes_CadastrarVagasModelId",
                table: "Estudantes");

            migrationBuilder.DropColumn(
                name: "CadastrarVagasModelId",
                table: "Estudantes");

            migrationBuilder.DropColumn(
                name: "CurriculoId",
                table: "Estudantes");
        }
    }
}
