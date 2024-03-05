using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.OrderProducts
{
    public class OrderProductRepository : EntityBaseRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly AppDbContext _context;
        public OrderProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderProduct> GetOrderProductById(int id)
        {
            var order = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(op => op.Id == id);

            return order;
        }

        public async Task AddOrderProduct(int id, OrderProduct orderProduct)
        {
            var newOrderProduct = new OrderProduct()
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity
            };

            await _context.OrderProducts.AddAsync(newOrderProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderProductsAsync(int id, OrderProduct orderProduct)
        {
            var oldOrderProduct = await _context.OrderProducts
                .Include(o => o.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(op => op.Id == id);

            if(oldOrderProduct != null)
            {
                oldOrderProduct.Id = orderProduct.Id;
                oldOrderProduct.OrderId = orderProduct.OrderId;
                oldOrderProduct.ProductId = orderProduct.ProductId;
                oldOrderProduct.Quantity = orderProduct.Quantity;

                if(oldOrderProduct.Order != null){
                    var order = await _context.Orders.FindAsync(orderProduct.Order.Id);
                    if(order != null)
                    {
                        oldOrderProduct.Order = order;
                    }
                }

                if (oldOrderProduct.Product != null)
                {
                    var product = await _context.Products.FindAsync(orderProduct.Product.Id);
                    if (product != null)
                    {
                        oldOrderProduct.Product = product;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
