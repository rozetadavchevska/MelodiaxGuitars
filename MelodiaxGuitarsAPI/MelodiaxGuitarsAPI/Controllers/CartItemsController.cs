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
using MelodiaxGuitarsAPI.Repositories.ShoppingCarts;
using System.Security.Claims;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public CartItemsController(ICartItemRepository cartItemRepository, IMapper mapper, IShoppingCartRepository shoppingCartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _shoppingCartRepository = shoppingCartRepository;
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

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            // Retrieve the shopping cart for the user
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserId(userId);
            if (shoppingCart == null)
            {
                // Create a new shopping cart if it doesn't exist
                shoppingCart = new ShoppingCart { UserId = userId };
                await _shoppingCartRepository.AddShoppingCartAsync(shoppingCart);
            }

            // Ensure CartItems collection is initialized
            shoppingCart.CartItems ??= new List<CartItem>();

            // Add the newly created cart item to the shopping cart
            shoppingCart.CartItems.Add(cartItem);

            // Update the shopping cart in the database
            await _shoppingCartRepository.UpdateShoppingCartItemsAsync(shoppingCart.Id, cartItem.Id);

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
