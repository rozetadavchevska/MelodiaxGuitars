using MelodiaxGuitarsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class CartItemDto
    {
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
