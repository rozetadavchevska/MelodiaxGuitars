using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using MelodiaxGuitarsAPI.Utilities;
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

        public async Task<User> GetUserById(string id)
        {
            var user = await _context.Users
                .Include(s => s.ShoppingCart)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetUserByEmail(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddUserAsync(User user)
        {
            string hashedPassword = PasswordHasher.HashPassword(user.PasswordHash);
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                ShoppingCartId = Guid.NewGuid().ToString()
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var newShoppingCart = new ShoppingCart
            {
                UserId = newUser.Id, 
                User = newUser
            };

            await _context.ShoppingCarts.AddAsync(newShoppingCart);
            await _context.SaveChangesAsync();

            newUser.ShoppingCartId = newShoppingCart.Id;
            newUser.ShoppingCart = newShoppingCart;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(string id, User user)
        {
            var oldUser = await _context.Users
                .Include(sc => sc.ShoppingCart)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (oldUser != null)
            {
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Email = user.Email;
                oldUser.PasswordHash = oldUser.PasswordHash;
                oldUser.PhoneNumber = user.PhoneNumber;
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

        public async Task UpdateUserOrdersAsync(string userId, string orderId)
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
