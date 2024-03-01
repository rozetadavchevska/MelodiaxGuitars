using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Orders
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        Task UpdateOrderAsync(int id, Order order);
    }
}
