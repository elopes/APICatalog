using APICatalog.Data;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDBContext _context;

        public CategoryRepository(CatalogDBContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id)
                           ?? throw new ArgumentException(message: $"Categoria com Id: {id} não foi encontrada");
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category == null ? throw new ArgumentException(message: $"Categoria com Id: {id} não foi encontrada") : category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var cat = await GetById(category.CategoryId);
            if (cat == null)
            {
                throw new ArgumentException(message: $"Categoria com Id: {category.CategoryId} não foi encontrada");
            }
            // Atualiza os valores da categoria
            _context.Entry(cat).CurrentValues.SetValues(category);
            _context.Categories.Update(cat);
            await _context.SaveChangesAsync();
            return cat;
        }
    }
}
