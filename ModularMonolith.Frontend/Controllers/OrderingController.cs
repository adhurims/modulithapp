using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Frontend.Models;
using ModularMonolith.Frontend.Services;
using Ordering.Application.DTOs;
using System.Threading.Tasks;

namespace ModularMonolith.WebAPI.Controllers
{
    public class OrderingController : Controller
    {
        private readonly OrderingService _orderingService;

        public OrderingController(OrderingService orderingService)
        {
            _orderingService = orderingService;
        }

        // GET: Ordering
        public async Task<IActionResult> Index()
        {
            var orders = await _orderingService.GetAllOrdersAsync();
            return View(orders);
        }

        // GET: Ordering/Create
        public IActionResult Create() => View();

        // POST: Ordering/Create
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto order)
        {
            if (ModelState.IsValid)
            {
                await _orderingService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Ordering/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Ordering/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderDto order)
        {
            if (ModelState.IsValid)
            {
                await _orderingService.UpdateOrderAsync(id, order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Ordering/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Ordering/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderingService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ordering/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
