using System.ComponentModel.DataAnnotations;

namespace ModularMonolith.Frontend.Models
{
    public class EditUserModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
