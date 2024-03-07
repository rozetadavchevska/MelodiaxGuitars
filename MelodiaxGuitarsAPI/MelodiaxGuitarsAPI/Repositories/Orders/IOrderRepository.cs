using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Orders
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        Task<Order> GetOrderById(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(int id, Order order);
        Task UpdateOrderProductAsync(int orderId, int orderProductId);
    }
}
