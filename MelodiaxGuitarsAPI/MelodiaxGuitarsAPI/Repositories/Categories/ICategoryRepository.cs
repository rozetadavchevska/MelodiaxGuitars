using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Categories
{
    public interface ICategoryRepository : IEntityBaseRepository<Category>
    {
        Task UpdateCategoryAsync(int id, Category category);
    }
}
