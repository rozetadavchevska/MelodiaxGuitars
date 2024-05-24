using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.DTOs;
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

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return product;
        }

        public async Task AddProductAsync(Product product)
        {
            var brand = await _context.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Id == product.BrandId);
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            if (brand != null && category != null)
            {
                product.Brand = brand;
                product.Category = category;
            }

            await _context.Products.AddAsync(product);
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
                oldProduct.Price = product.Price;

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
