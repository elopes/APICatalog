using APICatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(CatalogDBContext db)
    {
        // Garante que o banco em memória está criado (para provedores relacionais usar db.Database.Migrate())
        await db.Database.EnsureCreatedAsync();

        if (!await db.Categories.AnyAsync())
        {
            var cat1 = new Category { Name = "Bebidas", Description = "Bebidas em geral", ImageUrl = "bebidas.jpg" };
            var cat2 = new Category { Name = "Lanches", Description = "Lanches rápidos", ImageUrl = "lanches.jpg" };

            db.Categories.AddRange(cat1, cat2);

            db.Products.AddRange(
                new Product { Name = "Refrigerante", Description = "Lata 350ml", Price = 6.5m, Category = cat1, ImageUrl = "refri.jpg", Stock = 150 },
                new Product { Name = "Suco", Description = "Suco natural", Price = 12m, Category = cat1, ImageUrl = "suco.jpg", Stock = 80 },
                new Product { Name = "Hambúrguer", Description = "Artesanal", Price = 25m, Category = cat2, ImageUrl = "burger.jpg", Stock = 40 }
            );

            await db.SaveChangesAsync();
        }
    }
}
