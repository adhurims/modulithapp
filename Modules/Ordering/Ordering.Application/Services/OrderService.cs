using Ordering.Domain.Entities;
using Ordering.Domain.Repositories; 
using Ordering.Application.Interfaces;

namespace Ordering.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            return true;
        }
    }
}
