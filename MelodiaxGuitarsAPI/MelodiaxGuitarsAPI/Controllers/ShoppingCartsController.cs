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
using MelodiaxGuitarsAPI.Repositories.Users;
using Microsoft.AspNetCore.Authorization;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ShoppingCartsController(IShoppingCartRepository shoppingCartRepository, IMapper mapper, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(string id)
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
        public async Task<IActionResult> PutShoppingCart(string id, ShoppingCartDto shoppingCartDto)
        {
            var shoppingCartToUpdate = await _shoppingCartRepository.GetShoppingCartById(id);
            if(shoppingCartToUpdate == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<User>(shoppingCartDto.UserId);
            shoppingCartToUpdate.User = user;
            shoppingCartToUpdate.UserId = user.Id;

            await _shoppingCartRepository.UpdateShoppingCartAsync(id, shoppingCartToUpdate);
            await _userRepository.UpdateUserAsync(user.Id, user);
            return NoContent();
        }

        // POST: api/ShoppingCarts
        /*[HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> PostShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartDto);
            await _shoppingCartRepository.AddShoppingCartAsync(shoppingCart);

            var createdShoppingCart = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return CreatedAtAction(nameof(GetShoppingCart), new { id = shoppingCart.Id }, createdShoppingCart);
        }*/

        // DELETE: api/ShoppingCarts/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShoppingCart(string id)
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
