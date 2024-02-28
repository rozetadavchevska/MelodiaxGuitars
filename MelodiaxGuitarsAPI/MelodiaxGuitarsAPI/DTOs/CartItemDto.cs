using MelodiaxGuitarsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class CartItemDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
