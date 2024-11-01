using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Commands.Handlers
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(DateTime.UtcNow);

            foreach (var item in request.Items)
            {
                order.AddItem(new OrderItem(item.ProductId, item.Quantity, item.Price));
            }

            await _orderRepository.AddAsync(order);
            return true;
        }
    }
}
