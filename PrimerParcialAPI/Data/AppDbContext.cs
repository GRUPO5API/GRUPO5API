using Microsoft.EntityFrameworkCore;
using PrimerParcialAPI.Models;

namespace PrimerParcialAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuramos la propiedad Price para evitar truncamiento
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // precision = 18, scale = 2
        }
    }
}