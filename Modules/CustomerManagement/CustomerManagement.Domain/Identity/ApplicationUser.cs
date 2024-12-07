using CustomerManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CustomerManagement.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int? CustomerId { get; set; } // Foreign key
        [JsonIgnore]
        public Customer Customer { get; set; } // Navigation property
    }
}
