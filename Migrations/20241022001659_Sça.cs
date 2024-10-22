using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class Sça : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaModelId",
                table: "Estudantes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudantes_EmpresaModelId",
                table: "Estudantes",
                column: "EmpresaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudantes_Empresas_EmpresaModelId",
                table: "Estudantes",
                column: "EmpresaModelId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudantes_Empresas_EmpresaModelId",
                table: "Estudantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudantes_EmpresaModelId",
                table: "Estudantes");

            migrationBuilder.DropColumn(
                name: "EmpresaModelId",
                table: "Estudantes");
        }
    }
}
