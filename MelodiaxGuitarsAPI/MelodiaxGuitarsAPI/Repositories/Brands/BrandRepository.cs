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

            if (brandDetails == null)
            {
                throw new Exception("Brand not found");
            }

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
                var product = await _context.Products.FirstOrDefaultAsync(b => b.Id == productId); 
                if (product != null)
                {

                    if(brand.Products == null)
                    {
                        brand.Products = new List<Product>();
                    }

                    if (!brand.Products.Any(p => p.Id == productId))
                    {
                        brand.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
