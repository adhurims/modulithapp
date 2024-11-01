using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public int StockLevel { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, int stockLevel, decimal price)
        {
            Name = name;
            StockLevel = stockLevel;
            Price = price;
        }

        public void UpdateStock(int quantity)
        {
            StockLevel += quantity;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }
    }
}
