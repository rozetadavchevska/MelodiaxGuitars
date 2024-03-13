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
            if (brand == null)
            {
                throw new ArgumentNullException(nameof(brand), "Brand object cannot be null.");
            }

            var newBrand = new Brand()
            {
                Name = brand.Name,
                Description = brand.Description
            };

            await _context.Brands.AddAsync(newBrand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(string id, Brand brand)
        {
            var oldBrand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == id);
            
            if (oldBrand != null)
            {
                oldBrand.Name = brand.Name;
                oldBrand.Description = brand.Description;

                foreach (var newProduct in brand.Products)
                {
                    if (!oldBrand.Products.Any(p => p.Id == newProduct.Id))
                    {
                        oldBrand.Products.Add(newProduct);
                    }
                }

                foreach (var oldProduct in oldBrand.Products.ToList())
                {
                    if (!brand.Products.Any(p => p.Id == oldProduct.Id))
                    {
                        oldBrand.Products.Remove(oldProduct);
                    }
                }

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
