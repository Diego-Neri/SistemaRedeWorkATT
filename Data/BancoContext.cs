using SistemaRedeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaRedeWork.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<EstudanteModel> Estudantes { get; set; }
        public DbSet<LoginEmpresaModel> LoginEmpresas { get; set; }
        public DbSet<LoginEstudanteModel> LoginEstudantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Exemplo de configuração com Fluent API
            modelBuilder.Entity<EmpresaModel>()
                .ToTable("Empresas")
                .HasKey(e => e.Id); // Configuração da chave primária

            modelBuilder.Entity<UsuarioModel>()
                .ToTable("Usuarios")
                .HasKey(u => u.Id); // Configuração da chave primária
                                    //
                                  
                modelBuilder.Entity<EstudanteModel>()
                .ToTable("Estudantes")
                .HasKey(u => u.Id); // Configuração da chave primária

            // Adicione mais configurações se necessário
        }
    }
}
