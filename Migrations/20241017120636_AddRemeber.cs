using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class AddRemeber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginEstudantes_Estudantes_EstudanteId",
                table: "LoginEstudantes");

            migrationBuilder.AlterColumn<string>(
                name: "ResetCode",
                table: "LoginEstudantes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "EstudanteId",
                table: "LoginEstudantes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginEstudantes_Estudantes_EstudanteId",
                table: "LoginEstudantes",
                column: "EstudanteId",
                principalTable: "Estudantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginEstudantes_Estudantes_EstudanteId",
                table: "LoginEstudantes");

            migrationBuilder.UpdateData(
                table: "LoginEstudantes",
                keyColumn: "ResetCode",
                keyValue: null,
                column: "ResetCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResetCode",
                table: "LoginEstudantes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "EstudanteId",
                table: "LoginEstudantes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginEstudantes_Estudantes_EstudanteId",
                table: "LoginEstudantes",
                column: "EstudanteId",
                principalTable: "Estudantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
