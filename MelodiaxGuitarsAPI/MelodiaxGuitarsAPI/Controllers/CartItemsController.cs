using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.CartItems;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
        {
            var cartItem = await _cartItemRepository.GetAllAsync();
            var cartItemDto = _mapper.Map<List<CartItemDto>>(cartItem);
            return Ok(cartItemDto);
        }

        // GET: api/CartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemDto>> GetCartItem(int id)
        {
            var cartItem = await _cartItemRepository.GetCartItemById(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            var cartItemDto = _mapper.Map<CartItemDto>(cartItem);
            return Ok(cartItemDto);
        }

        // PUT: api/CartItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItemDto cartItemDto)
        {
            var cartItemUpdate = await _cartItemRepository.GetCartItemById(id);
            if (cartItemUpdate == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<Product>(cartItemDto.Product);
            cartItemUpdate.Product = product;
            cartItemUpdate.Quantity = cartItemDto.Quantity;

            await _cartItemRepository.UpdateCartItemAsync(id, cartItemUpdate);
            return NoContent();
        }

        // POST: api/CartItems
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostCartItem(CartItemDto cartItemDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            await _cartItemRepository.AddCartItemAsync(cartItem);

            var createdCartItem = _mapper.Map<CartItemDto>(cartItem);

            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, createdCartItem);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItemToDelete = await _cartItemRepository.GetCartItemById(id);
            if (cartItemToDelete == null)
            {
                return NotFound();
            }

            await _cartItemRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
