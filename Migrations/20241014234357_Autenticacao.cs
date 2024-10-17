﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    /// <inheritdoc />
    public partial class Autenticacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "LoginEstudantes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "LoginEmpresas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "LoginEstudantes");

            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "LoginEmpresas");
        }
    }
}
