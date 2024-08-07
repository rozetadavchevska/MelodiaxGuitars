﻿using MelodiaxGuitarsAPI.Data;
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
        public async Task<Category> GetCategoryById(string id)
        {
            var category = await _context.Categories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            return category;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(string id, Category category)
        {
            var oldCategory = await _context.Categories.FindAsync(id);
            if(oldCategory != null)
            {   
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryProductsAsync(string categoryId, string productId)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id ==  categoryId);

            if(category != null)
            {
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);
                if (product != null)
                {
                    if(category.Products == null)
                    {
                        category.Products = new List<Product>();
                    }

                    if (!category.Products.Any(p => p.Id == productId))
                    {
                        category.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
