using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.OrderProducts
{
    public interface IOrderProductRepository : IEntityBaseRepository<OrderProduct>
    {
        Task UpdateOrderProductsAsync(int id, OrderProduct product);
    }
}
