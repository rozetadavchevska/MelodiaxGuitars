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

        public async Task<ShoppingCart> GetShoppingCartById(string id)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(u => u.User)
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            return shoppingCart;
        }

        public async Task<ShoppingCart> GetShoppingCartByUserId(string userId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(u => u.User)
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            return shoppingCart;
        }

        public async Task AddShoppingCartAsync(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShoppingCartAsync(string id, ShoppingCart shoppingCart)
        {
            var oldShoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.User)
                .Include(sc => sc.CartItems)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (oldShoppingCart != null)
            {
                oldShoppingCart.UserId = shoppingCart.Id;

                if(oldShoppingCart.User != null)
                {
                    var user = await _context.Users.FindAsync(shoppingCart.UserId);
                    if (user != null)
                    {
                        oldShoppingCart.User = user;
                    }
                }

                foreach (var newItem in shoppingCart.CartItems)
                {
                    var oldItem = oldShoppingCart.CartItems.FirstOrDefault(ci => ci.Id == newItem.Id);
                    if (oldItem != null)
                    {
                        oldItem.Quantity = newItem.Quantity;
                    }
                    else
                    {
                        oldShoppingCart.CartItems.Add(newItem);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateShoppingCartItemsAsync(string shoppingCartId, string cartItemId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.CartItems)
                .FirstOrDefaultAsync(s => s.Id == shoppingCartId);

            if(shoppingCart != null)
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if(cartItem != null)
                {
                    shoppingCart.CartItems.Add(cartItem);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
