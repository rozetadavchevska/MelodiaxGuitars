using MelodiaxGuitarsAPI.Data;
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

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(s => s.ShoppingCart)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                ShoppingCartId = user.ShoppingCartId
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
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

        public async Task UpdateUserOrdersAsync(int userId, int orderId)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if(user != null)
            {
                var order = await _context.Orders.FindAsync(orderId);
                if(order != null)
                {
                    user.Orders.Add(order);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
