﻿using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.CartItems
{
    public class CartItemRepository : EntityBaseRepository<CartItem>, ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
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