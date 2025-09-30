using Microsoft.EntityFrameworkCore;
using PrimerParcialAPI.Models;

namespace PrimerParcialAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SupportTicket> SupportTickets { get; set; } = null;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(e =>
            {
                e.HasIndex(x => new { x.StartAt, x.IsOnline });
                e.Property(x => x.Title).HasMaxLength(150);
                e.Property(x => x.Location).HasMaxLength(120);
            });
            // Configuramos la propiedad Price para evitar truncamiento
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // precision = 18, scale = 2
        }
    }
}    