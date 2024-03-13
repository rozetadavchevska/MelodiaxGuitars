using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.OrderProducts
{
    public interface IOrderProductRepository : IEntityBaseRepository<OrderProduct>
    {
        Task<OrderProduct> GetOrderProductById(string id);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task UpdateOrderProductsAsync(string id, OrderProduct product);
    }
}
