using Microsoft.AspNetCore.Mvc;
using Ordering.Application.DTOs;
using Ordering.Domain.Repositories;
using AutoMapper;
using Ordering.Domain.Entities;
using Inventory.Domain.Repositories;
using Ordering.Application.Services;
using Ordering.Application.Interfaces;

namespace ModularMonolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;

        public OrderingController(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository, IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                if (orders == null)
                {
                    return NotFound("No orders found.");
                }

                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                // Logimi i gabimit
                Console.WriteLine($"Error fetching orders: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();
            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate and update stock
            foreach (var item in createOrderDto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null || product.StockLevel < item.Quantity)
                {
                    return BadRequest($"Product {item.ProductId} is out of stock or insufficient quantity.");
                }

                product.StockLevel -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            // Process payment
            var paymentResult = await _paymentService.ProcessPayment(createOrderDto.TotalAmount);
            if (!paymentResult.IsSuccess)
            {
                return BadRequest("Payment processing failed.");
            }

            // Create order
            var order = _mapper.Map<Order>(createOrderDto);
            await _orderRepository.AddAsync(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Update order details
            order.OrderDate = updateOrderDto.OrderDate;

            // Clear and update items
            order.Items.Clear();
            foreach (var item in updateOrderDto.Items)
            {
                var orderItem = new OrderItem(item.ProductId, item.Quantity, item.Price);
                order.AddItem(orderItem);
            }

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(order);
            return NoContent();
        }
    }
}
