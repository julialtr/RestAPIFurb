using RestAPIFurb.Models;

namespace RestAPIFurb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Administrador> administradores { get; set; }
        public DbSet<Comanda> comandas { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<ComandaProduto> comandasProdutos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComandaProduto>().HasKey(cp => new { cp.IdProduto, cp.IdComanda });
            modelBuilder.Entity<ComandaProduto>().HasOne(cp => cp.Produto).WithMany(p => p.Comandas).HasForeignKey(cp => cp.IdProduto);
            modelBuilder.Entity<ComandaProduto>().HasOne(cp => cp.Comanda).WithMany(c => c.Produtos).HasForeignKey(cp => cp.IdComanda);

            modelBuilder.Entity<Comanda>().HasOne(c => c.Usuario).WithMany(u => u.Comandas).HasForeignKey(c => c.UsuarioId);
        }
    }
}
