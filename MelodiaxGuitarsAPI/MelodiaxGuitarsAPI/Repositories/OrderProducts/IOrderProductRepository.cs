using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.OrderProducts
{
    public interface IOrderProductRepository : IEntityBaseRepository<OrderProduct>
    {
        Task<OrderProduct> GetOrderProductById(int id);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task UpdateOrderProductsAsync(int id, OrderProduct product);
    }
}
