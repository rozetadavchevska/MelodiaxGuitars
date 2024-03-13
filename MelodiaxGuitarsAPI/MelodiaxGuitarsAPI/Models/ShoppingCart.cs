using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
