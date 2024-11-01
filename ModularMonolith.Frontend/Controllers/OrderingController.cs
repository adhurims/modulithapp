using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.DTOs;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace ModularMonolith.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderingController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        //{
        //    var orders = await _orderRepository.GetAllAsync();
        //    var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
        //    return Ok(orderDtos);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var order = new Order(createOrderDto.OrderDate);
            foreach (var item in createOrderDto.Items)
            {
                var orderItem = new OrderItem(item.ProductId, item.Quantity, item.Price);
                order.AddItem(orderItem);
            }

            await _orderRepository.AddAsync(order);

            var orderDto = _mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            //order.OrderDate = updateOrderDto.OrderDate;

            // Update the items if needed (this is a basic example)
            order.Items.Clear();
            foreach (var item in updateOrderDto.Items)
            {
                var orderItem = new OrderItem(item.ProductId, item.Quantity, item.Price);
                order.AddItem(orderItem);
            }

           // await _orderRepository.UpdateAsync(order);
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

           // await _orderRepository.DeleteAsync(order);
            return NoContent();
        }
    }
}
