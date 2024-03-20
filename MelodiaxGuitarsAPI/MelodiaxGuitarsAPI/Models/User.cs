using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First name is required")]
        [NotNull]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last name is required")]
        [NotNull]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required")]
        [NotNull]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "City is required")]
        [NotNull]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country is required")]
        [NotNull]
        public string Country { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public string ShoppingCartId { get; set; } = string.Empty;
        public ShoppingCart? ShoppingCart { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
