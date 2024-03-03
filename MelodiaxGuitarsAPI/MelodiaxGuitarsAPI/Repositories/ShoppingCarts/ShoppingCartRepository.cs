using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.ShoppingCarts
{
    public class ShoppingCartRepository : EntityBaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateShoppingCartAsync(int id, ShoppingCart shoppingCart)
        {
            var oldShoppingCart = await _context.ShoppingCarts
                .Include(u => u.User)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (oldShoppingCart != null)
            {
                oldShoppingCart.Id = shoppingCart.Id;
                oldShoppingCart.UserId = shoppingCart.Id;

                if(oldShoppingCart.User != null)
                {
                    var user = await _context.Users.FindAsync(shoppingCart.User.Id);
                    if (user != null)
                    {
                        oldShoppingCart.User = user;
                    }
                }

                if(oldShoppingCart.CartItems != null)
                {
                    foreach(var newItem in shoppingCart.CartItems)
                    {
                        var oldItem = oldShoppingCart.CartItems.FirstOrDefault(sc => sc.Id == newItem.Id);
                        if(oldItem != null)
                        {
                            oldItem.Id = newItem.Id;
                        } 
                        else
                        {
                            oldShoppingCart.CartItems.Add(newItem);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
