using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.CartItems
{
    public interface ICartItemRepository : IEntityBaseRepository<CartItem>
    {
        Task UpdateCartItemAsync(int id, CartItem item);
    }
}
