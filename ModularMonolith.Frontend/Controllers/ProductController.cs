using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ModularMonolith.WebAPI.Controllers
{
    // Controllers/ProductController.cs
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Frontend.Models;
    using ModularMonolith.Frontend.Services;
    using System.Threading.Tasks;

    public class ProductController : Controller
    {
        private readonly InventoryService _inventoryService;

        public ProductController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _inventoryService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
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
    }
}