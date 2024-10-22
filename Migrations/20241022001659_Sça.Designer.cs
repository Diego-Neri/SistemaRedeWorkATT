﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaRedeWork.Data;

#nullable disable

namespace SistemaRedeWork.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20241022001659_Sça")]
    partial class Sça
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SistemaRedeWork.Models.Arquivos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Dados")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Arquivos");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.CurriculoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Certificado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Curso")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Educacao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Experiencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Habilidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Idioma")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Objetivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Semestre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Universidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Curriculo");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.EmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConfirmarSenha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Linkedin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Empresas", (string)null);
                });

            modelBuilder.Entity("SistemaRedeWork.Models.EstudanteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConfirmarSenha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Curso")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EmpresaModelId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Instituicao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Semestre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaModelId");

                    b.ToTable("Estudantes", (string)null);
                });

            modelBuilder.Entity("SistemaRedeWork.Models.LoginEmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJ")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("longtext");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ResetCode")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ResetCodeExpiration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("LoginEmpresas");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.LoginEstudanteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EstudanteId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ResetCode")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ResetCodeExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EstudanteId");

                    b.ToTable("LoginEstudantes");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("SistemaRedeWork.Models.EstudanteModel", b =>
                {
                    b.HasOne("SistemaRedeWork.Models.EmpresaModel", null)
                        .WithMany("Estudantes")
                        .HasForeignKey("EmpresaModelId");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.LoginEmpresaModel", b =>
                {
                    b.HasOne("SistemaRedeWork.Models.EmpresaModel", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.LoginEstudanteModel", b =>
                {
                    b.HasOne("SistemaRedeWork.Models.EstudanteModel", "Estudante")
                        .WithMany()
                        .HasForeignKey("EstudanteId");

                    b.Navigation("Estudante");
                });

            modelBuilder.Entity("SistemaRedeWork.Models.EmpresaModel", b =>
                {
                    b.Navigation("Estudantes");
                });
#pragma warning restore 612, 618
        }
    }
}
