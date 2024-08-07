﻿using MelodiaxGuitarsAPI.Data;
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

        public async Task<CartItem> GetCartItemById(string id)
        {
            var cartItem = await _context.CartItems
                .Include(p => p.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cartItem == null)
            {
                throw new Exception("Cart Item not found");
            }

            return cartItem;
        }

        public async Task AddCartItemAsync(CartItem item)
        {
            if (await _context.CartItems.AnyAsync(ci => ci.Id == item.Id))
            {
                throw new Exception("Item already exists");
            }

            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(string id, CartItem item)
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
