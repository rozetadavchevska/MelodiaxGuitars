using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.Orders
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(string id)
        {
            var order = await _context.Orders
                .Include(u => u.User)
                .Include(op => op.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(string id, Order order)
        {
            var oldOrder = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);
            
            if(oldOrder != null)
            {
                oldOrder.Id = order.Id;
                oldOrder.UserId = order.UserId;
                oldOrder.SubtotalCost = order.SubtotalCost;
                oldOrder.Shipping = order.Shipping;
                oldOrder.ShippingCost = order.ShippingCost;
                oldOrder.TotalCost = order.TotalCost;

                if(order.User != null)
                {
                    var user = await _context.Users.FindAsync(order.User.Id);
                    if (user != null)
                    {
                        oldOrder.User = user;
                    }
                }

                if(oldOrder.OrderProducts != null)
                {
                    foreach(var newOrderProduct in order.OrderProducts)
                    {
                        var oldOrderProduct = oldOrder.OrderProducts.FirstOrDefault(op => op.OrderId == newOrderProduct.OrderId);
                        if(oldOrderProduct != null)
                        {
                            oldOrderProduct.ProductId = newOrderProduct.ProductId;
                            oldOrderProduct.Quantity = newOrderProduct.Quantity;
                        } 
                        else
                        {
                            oldOrder.OrderProducts.Add(newOrderProduct);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderProductAsync(string orderId, string orderProductId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            var orderProduct = await _context.OrderProducts.FindAsync(orderProductId);

            order.OrderProducts.Add(orderProduct);

            await _context.SaveChangesAsync();
        }
    }
}
