using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Products;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;
using System.Drawing;
using MelodiaxGuitarsAPI.Repositories.Brands;
using MelodiaxGuitarsAPI.Repositories.Categories;
using Microsoft.AspNetCore.Authorization;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var product = await _productRepository.GetAllAsync();
            var productDto = _mapper.Map<List<ProductDto>>(product);
            return Ok(productDto);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> PutProduct(string id, ProductDto productDto)
        {
            var productToUpdate = await _productRepository.GetProductById(id);
            if(productToUpdate == null)
            {
                return NotFound();
            }

            /*var brand = _mapper.Map<Brand>(productDto.Brand);
            var category = _mapper.Map<Category>(productDto.Category);*/

            /*productToUpdate.Brand = brand;
            productToUpdate.Category = category;*/
            productToUpdate.Name = productDto.Name;
            productToUpdate.Description = productDto.Description;
            productToUpdate.Model = productDto.Model;
            productToUpdate.Type = productDto.Type;
            productToUpdate.Hand = productDto.Hand;
            productToUpdate.BodyShape = productDto.BodyShape;
            productToUpdate.Color = productDto.Color;
            productToUpdate.Top = productDto.Top;
            productToUpdate.SidesAndBack = productDto.SidesAndBack;
            productToUpdate.Neck = productDto.Neck;
            productToUpdate.Nut = productDto.Nut;
            productToUpdate.Fingerboard = productDto.Fingerboard;
            productToUpdate.Strings = productDto.Strings;
            productToUpdate.Tuners = productDto.Tuners;
            productToUpdate.Bridge = productDto.Bridge;
            productToUpdate.Controls = productDto.Controls;
            productToUpdate.Pickups = productDto.Pickups;
            productToUpdate.PickupSwitch = productDto.PickupSwitch;
            productToUpdate.Cutaway = productDto.Cutaway;
            productToUpdate.Pickguard = productDto.Pickguard;
            productToUpdate.Case = productDto.Case;
            productToUpdate.ScaleLength = productDto.ScaleLength;
            productToUpdate.Width = productDto.Width;
            productToUpdate.Depth = productDto.Depth;
            productToUpdate.Weight = productDto.Weight;
            productToUpdate.ImageUrl = productDto.ImageUrl;
            productToUpdate.Price = productDto.Price;

            await _productRepository.UpdateProductAsync(id, productToUpdate);
            await _brandRepository.UpdateBrandProductsAsync(productToUpdate.Brand.Id, id);
            await _categoryRepository.UpdateCategoryProductsAsync(productToUpdate.Category.Id, id);
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Id = Guid.NewGuid().ToString();
            await _productRepository.AddProductAsync(product);
            await _brandRepository.UpdateBrandProductsAsync(product.BrandId, product.Id);
            await _categoryRepository.UpdateCategoryProductsAsync(product.CategoryId, product.Id);

            var createdProduct = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, createdProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var productToDelete = await _productRepository.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
