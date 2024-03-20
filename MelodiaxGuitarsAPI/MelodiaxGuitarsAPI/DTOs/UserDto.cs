using MelodiaxGuitarsAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class UserDto
    {
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public required string Email { get; set; } 
        public required string PasswordHash { get; set; } 
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; } 
        public required string Country { get; set; }
    }
}
