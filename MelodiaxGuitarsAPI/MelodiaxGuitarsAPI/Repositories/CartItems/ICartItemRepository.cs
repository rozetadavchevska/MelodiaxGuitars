using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.CartItems
{
    public interface ICartItemRepository : IEntityBaseRepository<CartItem>
    {
        Task<CartItem> GetCartItemById(string id);
        Task AddCartItemAsync(CartItem item);
        Task UpdateCartItemAsync(string id, CartItem item);
    }
}
