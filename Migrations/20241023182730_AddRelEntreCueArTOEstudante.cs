using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class AddRelEntreCueArTOEstudante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstudanteId",
                table: "Curriculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstudanteId",
                table: "Arquivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_EstudanteId",
                table: "Curriculo",
                column: "EstudanteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EstudanteId",
                table: "Arquivos",
                column: "EstudanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Estudantes_EstudanteId",
                table: "Arquivos",
                column: "EstudanteId",
                principalTable: "Estudantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculo_Estudantes_EstudanteId",
                table: "Curriculo",
                column: "EstudanteId",
                principalTable: "Estudantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Estudantes_EstudanteId",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Curriculo_Estudantes_EstudanteId",
                table: "Curriculo");

            migrationBuilder.DropIndex(
                name: "IX_Curriculo_EstudanteId",
                table: "Curriculo");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_EstudanteId",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "EstudanteId",
                table: "Curriculo");

            migrationBuilder.DropColumn(
                name: "EstudanteId",
                table: "Arquivos");
        }
    }
}
