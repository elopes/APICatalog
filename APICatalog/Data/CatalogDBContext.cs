using APICatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Data
{
    public class CatalogDBContext : DbContext
    {
        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unicidade
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.CategoryId, p.Name })
                .IsUnique();

            // Precisão
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // (Opcional) Checks — efetivos em alguns bancos relacionais
            modelBuilder.Entity<Product>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Products_Price_Positive", "Price > 0");
                    t.HasCheckConstraint("CK_Products_Stock_NonNegative", "Stock >= 0");
                });
        }
    }
}
