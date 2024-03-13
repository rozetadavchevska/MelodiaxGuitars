using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Categories
{
    public interface ICategoryRepository : IEntityBaseRepository<Category>
    {
        Task<Category> GetCategoryById(string id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(string id, Category category);
        Task UpdateCategoryProductsAsync(string categoryId, string productId);
    }
}
