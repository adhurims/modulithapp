using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.Events
{
    public class CustomerCreatedEvent
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public CustomerCreatedEvent(int customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }
    }
}
