using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.ShoppingCarts
{
    public interface IShoppingCartRepository : IEntityBaseRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCartById(string id);
        Task<ShoppingCart> GetShoppingCartByUserId(string userId);
        Task AddShoppingCartAsync(ShoppingCart shoppingCart); 
        Task UpdateShoppingCartAsync(string id, ShoppingCart shoppingCart);
        Task UpdateShoppingCartItemsAsync(string shoppingCartId, string cartItemId);
    }
}
