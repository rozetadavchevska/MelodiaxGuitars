using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.ShoppingCarts
{
    public interface IShoppingCartRepository : IEntityBaseRepository<ShoppingCart>
    {
        Task UpdateShoppingCartAsync(int id, ShoppingCart shoppingCart);
    }
}
