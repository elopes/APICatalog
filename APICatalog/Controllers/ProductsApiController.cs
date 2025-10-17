using APICatalog.Dtos;
using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;
using APICatalog.Repositories.Interfaces;

namespace APICatalog.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsApiController(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> Get()
        {
            var products = await _repo.GetAllProducts();
            var result = products
                .Select(p => new ProductItemDto(p.ProductId, p.Name, p.Price, p.Stock, p.ImageUrl))
                .ToList();
            return Ok(result);
        }

        // GET api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                var p = await _repo.GetById(id);
                var dto = new ProductDto(p.ProductId, p.Name, p.Description, p.Price, p.ImageUrl, p.Stock, p.CategoryId, p.Category?.Name);
                return Ok(dto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] CreateUpdateProductDto product)
        {
            var entity = new Product
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock
            };

            var created = await _repo.AddProduct(entity);
            var dto = new ProductDto(created.ProductId, created.Name, created.Description, created.Price, created.ImageUrl, created.Stock, created.CategoryId, null);
            return CreatedAtAction(nameof(Get), new { id = created.ProductId }, dto);
        }

        // PUT api/products/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> Put(int id, [FromBody] CreateUpdateProductDto product)
        {
            try
            {
                var entity = await _repo.GetById(id);
                entity.CategoryId = product.CategoryId;
                entity.Name = product.Name;
                entity.Description = product.Description;
                entity.Price = product.Price;
                entity.ImageUrl = product.ImageUrl;
                entity.Stock = product.Stock;

                var updated = await _repo.UpdateProduct(entity);
                var dto = new ProductDto(updated.ProductId, updated.Name, updated.Description, updated.Price, updated.ImageUrl, updated.Stock, updated.CategoryId, updated.Category?.Name);
                return Ok(dto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
