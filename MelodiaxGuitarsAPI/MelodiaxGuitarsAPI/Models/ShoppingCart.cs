using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [NotNull]
        public User User {  get; set; } = new User();
        public ICollection<CartItem>? Items { get; set; }
    }
}
