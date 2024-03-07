using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.ShoppingCarts;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartsController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        // GET: api/ShoppingCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartDto>>> GetShoppingCarts()
        {
            var shoppingCart = await _shoppingCartRepository.GetAllAsync();
            var shoppingCartDto = _mapper.Map<List<ShoppingCartDto>>(shoppingCart);
            return Ok(shoppingCartDto);
        }

        // GET: api/ShoppingCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(int id)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartById(id);
            if(shoppingCart == null)
            {
                return NotFound();
            }
            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return Ok(shoppingCartDto);
        }

        // PUT: api/ShoppingCarts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart(int id, ShoppingCartDto shoppingCartDto)
        {
            var shoppingCartToUpdate = await _shoppingCartRepository.GetShoppingCartById(id);
            if(shoppingCartToUpdate == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<User>(shoppingCartDto.User);
            shoppingCartToUpdate.User = user;

            await _shoppingCartRepository.UpdateShoppingCartAsync(id, shoppingCartToUpdate);
            return NoContent();
        }

        // POST: api/ShoppingCarts
        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> PostShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            await _shoppingCartRepository.AddShoppingCartAsync(shoppingCart);

            var createdShoppingCart = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return CreatedAtAction(nameof(GetShoppingCart), new { id = shoppingCart.Id }, createdShoppingCart);
        }

        // DELETE: api/ShoppingCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartById(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            await _shoppingCartRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
