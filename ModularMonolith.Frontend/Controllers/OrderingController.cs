using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModularMonolith.Frontend.Services;
using Ordering.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModularMonolith.Frontend.Controllers
{
    public class OrderingController : Controller
    {
        private readonly OrderingService _orderingService;

        public OrderingController(OrderingService orderingService)
        {
            _orderingService = orderingService;
        }

        // List of Orders
        public async Task<IActionResult> List()
        {
            var orders = await _orderingService.GetAllOrdersAsync();
            return View(orders);
        }

        // Order Details
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await _orderingService.GetAvailableProductsAsync();
            var model = new CreateOrderDto
            {
                OrderDate = DateTime.Now,
                Items = new List<CreateOrderItemDto>(),
                ProductList = products.Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name // Use appropriate product property for display
                })
            };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto model)
        {
            if (ModelState.IsValid)
            {
                var paymentResult = await _orderingService.ProcessPaymentAsync(model);

                if (paymentResult.IsSuccess)
                {
                    await _orderingService.CreateOrderAsync(model);
                    return RedirectToAction("List");
                }

                ModelState.AddModelError("", "Payment failed. Please try again.");
            }

            model.Products = await _orderingService.GetAvailableProductsAsync(); // Reload products
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var model = new UpdateOrderDto
            {
                OrderDate = order.OrderDate,
                Items = order.Items.Select(item => new UpdateOrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateOrderDto model)
        {
            if (ModelState.IsValid)
            {
                await _orderingService.UpdateOrderAsync(id, model);
                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderingService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderingService.DeleteOrderAsync(id);
            return RedirectToAction("List");
        }
    }
}
