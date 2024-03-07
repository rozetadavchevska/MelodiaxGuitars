using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Categories
{
    public interface ICategoryRepository : IEntityBaseRepository<Category>
    {
        Task<Category> GetCategoryById(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(int id, Category category);
        Task UpdateCategoryProductsAsync(int categoryId, int productId);
    }
}
