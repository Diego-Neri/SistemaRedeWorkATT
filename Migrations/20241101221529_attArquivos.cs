using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class attArquivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Estudantes_EstudanteId",
                table: "Arquivos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arquivos",
                table: "Arquivos");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_EstudanteId",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "EstudanteId",
                table: "Arquivos");

            migrationBuilder.RenameTable(
                name: "Arquivos",
                newName: "ARQUIVOS");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ARQUIVOS",
                newName: "DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "Dados",
                table: "ARQUIVOS",
                newName: "DADOS");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "ARQUIVOS",
                newName: "CONTENTTYPE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ARQUIVOS",
                newName: "ID_ESTUDANTE");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRICAO",
                table: "ARQUIVOS",
                type: "VARCHAR(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DADOS",
                table: "ARQUIVOS",
                type: "BLOB",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.AlterColumn<string>(
                name: "CONTENTTYPE",
                table: "ARQUIVOS",
                type: "VARCHAR(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ID_ESTUDANTE",
                table: "ARQUIVOS",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ARQUIVOS",
                table: "ARQUIVOS",
                column: "ID_ESTUDANTE");

            migrationBuilder.AddForeignKey(
                name: "FK_ARQUIVOS_Estudantes_ID_ESTUDANTE",
                table: "ARQUIVOS",
                column: "ID_ESTUDANTE",
                principalTable: "Estudantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARQUIVOS_Estudantes_ID_ESTUDANTE",
                table: "ARQUIVOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ARQUIVOS",
                table: "ARQUIVOS");

            migrationBuilder.RenameTable(
                name: "ARQUIVOS",
                newName: "Arquivos");

            migrationBuilder.RenameColumn(
                name: "DESCRICAO",
                table: "Arquivos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "DADOS",
                table: "Arquivos",
                newName: "Dados");

            migrationBuilder.RenameColumn(
                name: "CONTENTTYPE",
                table: "Arquivos",
                newName: "ContentType");

            migrationBuilder.RenameColumn(
                name: "ID_ESTUDANTE",
                table: "Arquivos",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Arquivos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Dados",
                table: "Arquivos",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Arquivos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Arquivos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "EstudanteId",
                table: "Arquivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arquivos",
                table: "Arquivos",
                column: "Id");

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
        }
    }
}
