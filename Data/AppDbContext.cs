using DriveNow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Locacao> Locacaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasIndex(p => p.Cpf)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
