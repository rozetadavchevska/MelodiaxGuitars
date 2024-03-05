using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Orders
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        Task<Order> GetOrderById(int id);
        Task AddOrderAsync(int id, Order order);
        Task UpdateOrderAsync(int id, Order order);
    }
}
