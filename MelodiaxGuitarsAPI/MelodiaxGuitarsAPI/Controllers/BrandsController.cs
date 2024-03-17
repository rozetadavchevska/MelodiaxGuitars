using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Brands;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandsController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        // GET: api/Brands
        [HttpGet]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            var brandDto = _mapper.Map<List<BrandDto>>(brands);
            return Ok(brandDto);
        }

        // GET: api/Brands/5
        [HttpGet("{id}", Name = nameof(GetBrand))]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<BrandDto>> GetBrand(string id)
        {
            var brand = await _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }

            var brandDto = _mapper.Map<BrandDto>(brand);
            return Ok(brandDto);
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBrand(string id, BrandDto brandDto)
        {
            var brandUpdate = await _brandRepository.GetBrandById(id);
            if (brandUpdate == null)
            {
                return NotFound();
            }

            brandUpdate.Name = brandDto.Name;
            brandUpdate.Description = brandDto.Description;

            await _brandRepository.UpdateBrandAsync(id, brandUpdate);

            return NoContent();
        }

        // POST: api/Brands
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> PostBrand(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _brandRepository.AddBrandAsync(brand);

            var createdBrand = _mapper.Map<BrandDto>(brand);
            return CreatedAtRoute("GetBrand", new { id = brand.Id }, createdBrand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var brandToDelete = await _brandRepository.GetBrandById(id);
            if (brandToDelete == null)
            {
                return NotFound();
            }

            await _brandRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

