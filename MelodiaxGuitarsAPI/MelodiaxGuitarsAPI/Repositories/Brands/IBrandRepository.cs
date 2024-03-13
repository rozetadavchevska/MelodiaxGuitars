using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Brands
{
    public interface IBrandRepository : IEntityBaseRepository<Brand>
    {
        Task<Brand> GetBrandById(string id);
        Task AddBrandAsync(Brand brand);
        Task UpdateBrandAsync(string id, Brand brand);
        Task UpdateBrandProductsAsync(string brandId, string productId);
    }
}
