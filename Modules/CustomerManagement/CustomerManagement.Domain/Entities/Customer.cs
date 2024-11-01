using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }

        public Customer(string name, string email, string address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        public void UpdateContactInfo(string newEmail, string newAddress)
        {
            Email = newEmail;
            Address = newAddress;
        }
    }
}
