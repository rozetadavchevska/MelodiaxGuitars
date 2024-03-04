﻿using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.Users
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            var oldUser = await _context.Users
                .Include(sc => sc.ShoppingCart)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (oldUser != null)
            {
                oldUser.Id = user.Id;
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Email = user.Email;
                oldUser.Password = oldUser.Password;
                oldUser.Phone = user.Phone;
                oldUser.Address = user.Address;
                oldUser.City = user.City;
                oldUser.Country = user.Country;
                oldUser.ShoppingCartId = user.ShoppingCartId;

                if(oldUser.ShoppingCart != null)
                {
                    var shoppingCart = await _context.ShoppingCarts.FindAsync(user.ShoppingCart.Id);
                    if(shoppingCart != null)
                    {
                        oldUser.ShoppingCart = shoppingCart;
                    }
                }

                if(oldUser.Orders != null)
                {
                    foreach(var newOrder in user.Orders)
                    {
                        var order = oldUser.Orders.FirstOrDefault(o => o.Id ==  newOrder.Id);
                        if(order != null)
                        {
                            order.SubtotalCost = newOrder.SubtotalCost;
                            order.Shipping = newOrder.Shipping;
                            order.ShippingCost = newOrder.ShippingCost;
                            order.TotalCost = newOrder.TotalCost;
                        } 
                        else
                        {
                            oldUser.Orders.Add(newOrder);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}