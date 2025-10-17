using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;

namespace APICatalog.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            try
            {
                var product = await _repository.GetById(id.Value);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,Name,Description,Price,ImageUrl,Stock,Created")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var product = await _repository.GetById(id.Value);
                return View(product);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,Name,Description,Price,ImageUrl,Stock,Created")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateProduct(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var product = await _repository.GetById(id.Value);
                return View(product);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repository.DeleteProduct(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            var all = await _repository.GetAllProducts();
            return all.Any(e => e.ProductId == id);
        }
    }
}
