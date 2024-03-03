using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Products
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        Task UpdateProductAsync(int id, Product product);
    }
}
