using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Products
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductById(string id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(string id, Product product);
        Task UpdateProductOrdersAsync(string productId, string orderProductsId);
    }
}
