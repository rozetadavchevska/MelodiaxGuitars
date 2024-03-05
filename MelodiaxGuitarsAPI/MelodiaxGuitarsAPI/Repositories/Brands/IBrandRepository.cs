using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Brands
{
    public interface IBrandRepository : IEntityBaseRepository<Brand>
    {
        Task<Brand> GetBrandById(int id);
        Task AddBrandAsync(Brand brand);
        Task UpdateBrandAsync(int id, Brand brand);
        Task DeleteBrandAsync(int id);
    }
}
