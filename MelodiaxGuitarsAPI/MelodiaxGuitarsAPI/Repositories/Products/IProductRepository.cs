using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Products
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductById(int id);
        Task AddProductAsync(int id, Product product);
        Task UpdateProductAsync(int id, Product product);
    }
}
