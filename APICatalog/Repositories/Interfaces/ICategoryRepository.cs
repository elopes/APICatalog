using APICatalog.Models;

namespace APICatalog.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        // Async methods
        Task<List<Category>> GetAllCategories();
        Task<Category> GetById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
