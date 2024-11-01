using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class AddProductCommand : IRequest<bool>
    {
        public string Name { get; }
        public int StockLevel { get; }
        public decimal Price { get; }

        public AddProductCommand(string name, int stockLevel, decimal price)
        {
            Name = name;
            StockLevel = stockLevel;
            Price = price;
        }
    }
}
