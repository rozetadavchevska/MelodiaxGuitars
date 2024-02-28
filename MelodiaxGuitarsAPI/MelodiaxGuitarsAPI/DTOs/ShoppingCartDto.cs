using MelodiaxGuitarsAPI.Models;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class ShoppingCartDto
    {
        public UserDto User { get; set; }
        public ICollection<CartItemDto> Items { get; set; }
    }
}
