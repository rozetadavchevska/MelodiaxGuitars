using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Orders
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        Task<Order> GetOrderById(string id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(string id, Order order);
        Task UpdateOrderProductAsync(string orderId, string orderProductId);
    }
}
