using CustomerManagement.Domain.Identity;
using Ordering.Domain.Entities;
using System.Text.Json.Serialization;

namespace CustomerManagement.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Address { get;  set; }
        public string PhoneNumber { get;  set; }
        public string UserId { get; set; } // Foreign key
        [JsonIgnore]
        public ApplicationUser? User { get; set; } // Navigation property

        public Customer() { }
         
        // Constructor
        public Customer(string name, string email, string address, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public void UpdateContactInfo(string newEmail, string newAddress, string newPhoneNumber)
        {
            Email = newEmail;
            Address = newAddress;
            PhoneNumber = newPhoneNumber;
        }
    }
}
