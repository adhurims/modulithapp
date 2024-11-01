using MediatR;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands
{
    public class PlaceOrderCommand : IRequest<bool>
    {
        public List<OrderItemDto> Items { get; }

        public PlaceOrderCommand(List<OrderItemDto> items)
        {
            Items = items;
        }
    }
}
