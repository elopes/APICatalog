using APICatalog.Models;

namespace APICatalog.Repositories.Interfaces
{
    public interface IProductRepository
    {
        // Async methods
        Task<List<Product>> GetAllProducts();
        Task<Product> GetById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
            
    }
}

