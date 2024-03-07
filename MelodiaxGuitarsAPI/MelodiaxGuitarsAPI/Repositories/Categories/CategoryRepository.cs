using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace MelodiaxGuitarsAPI.Repositories.Categories
{
    public class CategoryRepository : EntityBaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task AddCategoryAsync(Category category)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var oldCategory = await _context.Categories.FindAsync(id);
            if(oldCategory != null)
            {
                oldCategory.Id = category.Id;
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryProductsAsync(int categoryId, int productId)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id ==  categoryId);

            if(category != null)
            {
                var product = await _context.Products.FindAsync(productId);
                if(product != null)
                {
                    category.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
