using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.CartItems
{
    public interface ICartItemRepository : IEntityBaseRepository<CartItem>
    {
        Task<CartItem> GetCartItemById(int id);
        Task AddCartItemAsync(CartItem item);
        Task UpdateCartItemAsync(int id, CartItem item);
        Task DeleteCartItemAsync(int id);
    }
}
