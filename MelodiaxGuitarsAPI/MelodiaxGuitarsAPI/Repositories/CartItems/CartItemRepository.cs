using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.CartItems
{
    public class CartItemRepository : EntityBaseRepository<CartItem>, ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            var cartItem = await _context.CartItems
                .Include(p => p.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            return cartItem;
        }

        public async Task AddCartItemAsync(CartItem item)
        {
            var newCartItem = new CartItem()
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };

            await _context.CartItems.AddAsync(newCartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(int id, CartItem item)
        {
            var oldItem = await _context.CartItems.FindAsync(id);
            if (oldItem != null)
            {
                oldItem.Id = item.Id;
                oldItem.ProductId = item.ProductId;
                oldItem.Quantity = item.Quantity;

                if (item.Product != null)
                {
                    var product = await _context.Products.FindAsync(item.Product.Id);
                    if(product != null)
                    {
                        oldItem.Product = product;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
