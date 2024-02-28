using MelodiaxGuitarsAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; } 
        public string Country { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
    }
}
