using APICatalog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
           List<Product> products = new List<Product>();
           
            products.Add(new Product { ProductId = 1, CategoryId = 1, Name = "Product 1", Description = "Description 1", Price = 9.99m, ImageUrl = "image1.jpg", Stock = 100 });
            products.Add(new Product { ProductId = 2, CategoryId = 2, Name = "Product 2", Description = "Description 2", Price = 19.99m, ImageUrl = "image2.jpg", Stock = 50 });
            
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = new Product { ProductId = 1, CategoryId = 1, Name = "Product 1", Description = "Description 1", Price = 9.99m, ImageUrl = "image1.jpg", Stock = 100 };
            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
