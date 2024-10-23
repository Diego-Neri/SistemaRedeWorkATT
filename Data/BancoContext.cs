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
        public DbSet<CurriculoModel> Curriculo { get; set; }
        public DbSet<Arquivos> Arquivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Configuração para a entidade EmpresaModel
            modelBuilder.Entity<EmpresaModel>()
                .ToTable("Empresas")
                .HasKey(e => e.Id); // Configuração da chave primária

            // Configuração para a entidade UsuarioModel
            modelBuilder.Entity<UsuarioModel>()
                .ToTable("Usuarios")
                .HasKey(u => u.Id); // Configuração da chave primária

            // Configuração para a entidade EstudanteModel
            modelBuilder.Entity<EstudanteModel>()
                .ToTable("Estudantes")
                .HasKey(u => u.Id); // Configuração da chave primária

            // Relacionamento um para um entre EstudanteModel e CurriculoModel
            modelBuilder.Entity<CurriculoModel>()
                .HasOne(c => c.Estudante)
                .WithOne(e => e.Curriculo)
                .HasForeignKey<CurriculoModel>(c => c.EstudanteId);

            // Relacionamento um para muitos entre EstudanteModel e Arquivos
            modelBuilder.Entity<Arquivos>()
                .HasOne(a => a.Estudante)
                .WithMany(e => e.Arquivos)
                .HasForeignKey(a => a.EstudanteId);

            // Adicione mais configurações, se necessário
        }
    }
}
