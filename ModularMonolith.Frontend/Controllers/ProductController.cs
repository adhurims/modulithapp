using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Services;
using Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace ModularMonolith.Frontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly InventoryService _inventoryService;

        public ProductController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> List()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _inventoryService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.CreateProductAsync(product);
                return RedirectToAction(nameof(List));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _inventoryService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDto product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.UpdateProductAsync(id, product);
                return RedirectToAction(nameof(List));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _inventoryService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [Route("Customer/DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryService.DeleteProductAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
