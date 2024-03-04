﻿using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;

namespace MelodiaxGuitarsAPI.Repositories.Categories
{
    public class CategoryRepository : EntityBaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
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
    }
}