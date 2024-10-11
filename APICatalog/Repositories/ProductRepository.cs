using APICatalog.Data;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogDBContext _dbContext;

        public ProductRepository(CatalogDBContext context)
        {
            _dbContext = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
             await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id)
                   ?? throw new ArgumentException(message: $"Produto com Id: {id} não foi encontrado");
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            Product? prod = await GetById(product.ProductId);
            if (prod == null)
            {
                throw new ArgumentException(message: $"Usuário para o Id: {product.ProductId} não foi encontrado");
            }

            // Atualiza os valores do produto
            _dbContext.Entry(prod).CurrentValues.SetValues(product);
            _dbContext.Products.Update(prod);
            await _dbContext.SaveChangesAsync();

            return prod;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product? prod = await GetById(id) ?? throw new ArgumentException(message: $"Usuário para o Id: {id} não foi encontrado");
            _dbContext.Products.Remove(prod);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
