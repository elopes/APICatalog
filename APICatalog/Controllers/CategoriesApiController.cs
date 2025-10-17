using APICatalog.Dtos;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICatalog.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesApiController : ControllerBase
{
    private readonly ICategoryRepository _repo;

    public CategoriesApiController(ICategoryRepository repo)
    {
        _repo = repo;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryItemDto>>> Get()
    {
        var categories = await _repo.GetAllCategories();
        var result = categories
            .Select(c => new CategoryItemDto(c.CategoryId, c.Name, c.ImageUrl))
            .ToList();
        return Ok(result);
    }

    // GET: api/categories/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        try
        {
            var category = await _repo.GetById(id);
            var dto = new CategoryDto(
                category.CategoryId,
                category.Name,
                category.Description,
                category.ImageUrl,
                category.Products.Select(p => new ProductItemDto(p.ProductId, p.Name, p.Price, p.Stock, p.ImageUrl)).ToList()
            );
            return Ok(dto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/categories
    [HttpPost]
    public async Task<ActionResult<CategoryItemDto>> Post([FromBody] CreateUpdateCategoryDto category)
    {
        var entity = new Category
        {
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl
        };

        var created = await _repo.AddCategory(entity);
        var dto = new CategoryItemDto(created.CategoryId, created.Name, created.ImageUrl);
        return CreatedAtAction(nameof(Get), new { id = created.CategoryId }, dto);
    }

    // PUT: api/categories/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryItemDto>> Put(int id, [FromBody] CreateUpdateCategoryDto category)
    {
        try
        {
            var entity = await _repo.GetById(id);
            entity.Name = category.Name;
            entity.Description = category.Description;
            entity.ImageUrl = category.ImageUrl;

            var updated = await _repo.UpdateCategory(entity);
            var dto = new CategoryItemDto(updated.CategoryId, updated.Name, updated.ImageUrl);
            return Ok(dto);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/categories/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repo.DeleteCategory(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
