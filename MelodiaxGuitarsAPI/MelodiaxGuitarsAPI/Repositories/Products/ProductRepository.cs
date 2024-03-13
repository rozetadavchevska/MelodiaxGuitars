using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Base;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Drawing;
using System.Runtime.Versioning;

namespace MelodiaxGuitarsAPI.Repositories.Products
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await _context.Products
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .Include(op => op.OrderProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                BrandId = product.BrandId,
                Model = product.Model,
                Type = product.Type,
                Hand = product.Hand,
                BodyShape = product.BodyShape,
                Color = product.Color,
                Top = product.Top,
                SidesAndBack = product.SidesAndBack,
                Neck = product.Neck,
                Nut = product.Nut,
                Fingerboard = product.Fingerboard,
                Strings = product.Strings,
                Tuners = product.Tuners,
                Bridge = product.Bridge,
                Controls = product.Controls,
                Pickups = product.Pickups,
                PickupSwitch = product.PickupSwitch,
                Cutaway = product.Cutaway,
                Pickguard = product.Pickguard,
                Case = product.Case,
                ScaleLength = product.ScaleLength,
                Width = product.Width,
                Depth = product.Depth,
                Weight = product.Weight,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            var oldProduct = await _context.Products
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(oldProduct != null)
            {
                oldProduct.Id = product.Id;
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.BrandId = product.BrandId;
                oldProduct.Model = product.Model;
                oldProduct.Type = product.Type;
                oldProduct.Hand = product.Hand;
                oldProduct.BodyShape = product.BodyShape;
                oldProduct.Color = product.Color;
                oldProduct.Top = product.Top;
                oldProduct.SidesAndBack = product.SidesAndBack;
                oldProduct.Neck = product.Neck;
                oldProduct.Nut = product.Nut;
                oldProduct.Fingerboard = product.Fingerboard;
                oldProduct.Strings = product.Strings;
                oldProduct.Tuners = product.Tuners;
                oldProduct.Bridge = product.Bridge;
                oldProduct.Controls = product.Controls;
                oldProduct.Pickups = product.Pickups;
                oldProduct.PickupSwitch = product.PickupSwitch;
                oldProduct.Cutaway = product.Cutaway;
                oldProduct.Pickguard = product.Pickguard;
                oldProduct.Case = product.Case;
                oldProduct.ScaleLength = product.ScaleLength;
                oldProduct.Width = product.Width;
                oldProduct.Depth = product.Depth;
                oldProduct.Weight = product.Weight;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.ImageUrl = product.ImageUrl;

                if(oldProduct.Brand != null)
                {
                    var brand = await _context.Brands.FindAsync(product.Brand.Id);
                    if(brand != null)
                    {
                        oldProduct.Brand = brand;
                    }
                }

                if (oldProduct.Category != null)
                {
                    var category = await _context.Categories.FindAsync(product.Category.Id);
                    if (category != null)
                    {
                        oldProduct.Category = category;
                    }
                }

                if(oldProduct.OrderProducts != null)
                {
                    foreach(var newProduct in product.OrderProducts)
                    {
                        var oldOrderProduct = oldProduct.OrderProducts.FirstOrDefault(op => op.ProductId == newProduct.ProductId);
                        if (oldOrderProduct != null)
                        {
                            oldOrderProduct.OrderId = newProduct.OrderId;
                            oldOrderProduct.Quantity = newProduct.Quantity;
                        }
                        else
                        {
                            oldProduct.OrderProducts.Add(newProduct);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }             
        }

        public async Task UpdateProductOrdersAsync(string productId, string orderProductsId)
        {
            var product = await _context.Products
                .Include(p => p.OrderProducts)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if(product != null)
            {
                var orderProducts = await _context.OrderProducts.FindAsync(orderProductsId);
                if(orderProducts != null)
                {
                    product.OrderProducts.Add(orderProducts);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
