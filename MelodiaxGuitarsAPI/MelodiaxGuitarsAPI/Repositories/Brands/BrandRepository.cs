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

        public async Task<Brand> GetBrandById(int id)
        {
            var brandDetails = await _context.Brands
                .Include(p => p.Products)
                .FirstOrDefaultAsync(b => b.Id == id);

            return brandDetails;
        }

        public async Task UpdateBrandAsync(int id, Brand brand)
        {
            var oldBrand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (oldBrand != null)
            {
                oldBrand.Name = brand.Name;
                oldBrand.Description = brand.Description;

                await _context.SaveChangesAsync();
            }
        }
    }
}
