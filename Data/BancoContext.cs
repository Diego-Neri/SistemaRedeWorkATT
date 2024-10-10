//using SistemaRedeWork.Data.Map;
using SistemaRedeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaRedeWork.Data {
    public class BancoContext : DbContext {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProductModel> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
