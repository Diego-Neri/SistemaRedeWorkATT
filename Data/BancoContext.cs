using SistemaRedeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaRedeWork.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {
        }
        public DbSet<EmpresaModel> Empresas { get; set; }
        public DbSet<EstudanteModel> Estudantes { get; set; }
        public DbSet<LoginEmpresaModel> LoginEmpresas { get; set; }
        public DbSet<LoginEstudanteModel> LoginEstudantes { get; set; }
        public DbSet<CurriculoModel> Curriculo { get; set; }
        public DbSet<ArquivoModel> Arquivos { get; set; }

        public DbSet<CadastrarVagasModel> Vagas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Configuração para a entidade EmpresaModel
            modelBuilder.Entity<EmpresaModel>()
                .ToTable("Empresas")
                .HasKey(e => e.Id); // Configuração da chave primária

            modelBuilder.Entity<EmpresaModel>()
                .HasIndex(e => new { e.Email, e.CNPJ, })
                .IsUnique();

            // Configuração para a entidade EstudanteModel
            modelBuilder.Entity<EstudanteModel>()
                .ToTable("Estudantes")
                .HasKey(u => u.Id);

            modelBuilder.Entity<EstudanteModel>()
                .HasIndex(e => new { e.Email, e.CPF })
                .IsUnique(); // Configuração da chave primária

            // Relacionamento um para um entre EstudanteModel e CurriculoModel
            modelBuilder.Entity<CurriculoModel>()
                .HasOne(c => c.Estudante)
                .WithOne(e => e.Curriculo)
                .HasForeignKey<CurriculoModel>(c => c.ID_ESTUDANTE)
                .OnDelete(DeleteBehavior.Cascade);


            // Relacionamento um para muitos entre EstudanteModel e Arquivos
            modelBuilder.Entity<ArquivoModel>()
                .HasOne(a => a.Estudante)
                .WithMany(e => e.Arquivos)
                .HasForeignKey(a => a.ID_ESTUDANTE);

            // Relacionamento um para muitos entre EmpresaModel e CadastrarVagasModel
            modelBuilder.Entity<CadastrarVagasModel>()
                .HasOne(v => v.Empresa)
                .WithMany(e => e.Vagas)
                .HasForeignKey(v => v.ID_EMPRESA)
                .OnDelete(DeleteBehavior.Cascade);  // Configuração de exclusão em cascata (opcional)

            // Adicione mais configurações, se necessário
        }
    }
}
