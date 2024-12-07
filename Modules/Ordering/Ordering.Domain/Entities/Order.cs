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
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order(DateTime orderDate)
        {
            OrderDate = orderDate;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            if (Items == null)
            {
                Items = new List<OrderItem>();
            }
            Items.Add(item);
        }
    }
}
