using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Categories;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var category = await _categoryRepository.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(category);
            return Ok(categoryDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(string id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCategory(string id, CategoryDto categoryDto)
        {
            var categoryToUpdate = await _categoryRepository.GetCategoryById(id);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate.Name = categoryDto.Name;
            categoryToUpdate.Description = categoryDto.Description;

            await _categoryRepository.UpdateCategoryAsync(id, categoryToUpdate);
            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<Category>> PostCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddCategoryAsync(category);
            var createdCategory = _mapper.Map<CategoryDto>(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, createdCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var categoryToDelete = await _categoryRepository.GetCategoryById(id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
