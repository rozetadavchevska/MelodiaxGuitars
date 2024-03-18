using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MelodiaxGuitarsAPI.Repositories.Brands
{
    public class BrandRepository : EntityBaseRepository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Brand> GetBrandById(string id)
        {
            var brandDetails = await _context.Brands
                .Include(p => p.Products)
                .FirstOrDefaultAsync(b => b.Id == id);

            return brandDetails;
        }


        public async Task AddBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(string id, Brand brand)
        {
            var oldBrand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (oldBrand != null)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBrandProductsAsync(string brandId, string productId)
        {
            var brand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == brandId);
            if (brand != null)
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    brand.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
