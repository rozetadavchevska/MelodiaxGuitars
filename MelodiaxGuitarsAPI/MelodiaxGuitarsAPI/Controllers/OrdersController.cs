using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MelodiaxGuitarsAPI.Data;
using MelodiaxGuitarsAPI.Models;
using MelodiaxGuitarsAPI.Repositories.Orders;
using AutoMapper;
using MelodiaxGuitarsAPI.DTOs;

namespace MelodiaxGuitarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var order = await _orderRepository.GetAllAsync();
            var orderDto = _mapper.Map<List<OrderDto>>(order);
            return Ok(orderDto);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, OrderDto orderDto)
        {
            var orderToUpdate = await _orderRepository.GetOrderById(id);
            if (orderToUpdate == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<User>(orderDto.User);
            orderToUpdate.User = user;
            orderToUpdate.SubtotalCost = orderDto.SubtotalCost;
            orderToUpdate.Shipping = orderDto.Shipping;
            orderToUpdate.ShippingCost = orderDto.ShippingCost;
            orderToUpdate.TotalCost = orderDto.TotalCost;

            await _orderRepository.UpdateOrderAsync(id, orderToUpdate);
            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddOrderAsync(order);
           
            var createdOrder = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, createdOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var orderToDelete = await _orderRepository.GetOrderById(id);
            if(orderToDelete == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
