using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.OrderProducts;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IMapper _mapper;

        public OrderProductsController(IOrderProductRepository orderProductRepository, IMapper mapper)
        {
            _orderProductRepository = orderProductRepository;
            _mapper = mapper;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProductDto>>> GetOrderProducts()
        {
            var orderProduct = await _orderProductRepository.GetAllAsync();
            var orderProductDto = _mapper.Map<List<OrderProductDto>>(orderProduct);
            return Ok(orderProductDto);
        }

        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductDto>> GetOrderProduct(string id)
        {
            var orderProduct = await _orderProductRepository.GetOrderProductById(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            var orderProductDto = _mapper.Map<OrderProductDto>(orderProduct);
            return Ok(orderProductDto);
        }

        // PUT: api/OrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct(string id, OrderProductDto orderProductDto)
        {
            var orderProductToUpdate = await _orderProductRepository.GetOrderProductById(id);
            if (orderProductToUpdate == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<Product>(orderProductDto.ProductId);
            var order = _mapper.Map<Order>(orderProductDto.OrderId);
            orderProductToUpdate.Product = product;
            orderProductToUpdate.Order = order;
            orderProductToUpdate.Quantity = orderProductToUpdate.Quantity;

            await _orderProductRepository.UpdateOrderProductsAsync(id, orderProductToUpdate);
            return NoContent();
        }

        // POST: api/OrderProducts
        [HttpPost]
        public async Task<ActionResult<OrderProductDto>> PostOrderProduct(OrderProductDto orderProductDto)
        {
            var orderProduct = _mapper.Map<OrderProduct>(orderProductDto);
            await _orderProductRepository.AddOrderProductAsync(orderProduct);
            
            var createdOrderProduct = _mapper.Map<OrderProductDto>(orderProduct);

            return CreatedAtAction(nameof(GetOrderProduct), new { id = orderProduct.Id }, createdOrderProduct);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct(string id)
        {
            var orderProduct = await _orderProductRepository.GetOrderProductById(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            await _orderProductRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
