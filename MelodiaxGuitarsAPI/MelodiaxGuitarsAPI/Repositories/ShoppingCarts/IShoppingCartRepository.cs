using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.ShoppingCarts
{
    public interface IShoppingCartRepository : IEntityBaseRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCartById(int id);
        Task AddShoppingCartAsync(ShoppingCart shoppingCart); 
        Task UpdateShoppingCartAsync(int id, ShoppingCart shoppingCart);
    }
}
