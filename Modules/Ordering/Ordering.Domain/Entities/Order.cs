using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{ 
    public interface IAggregateRoot { }
    public class Order : Entity, IAggregateRoot
    {
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(DateTime orderDate)
        {
            OrderDate = orderDate;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item) => Items.Add(item);
    }
}
