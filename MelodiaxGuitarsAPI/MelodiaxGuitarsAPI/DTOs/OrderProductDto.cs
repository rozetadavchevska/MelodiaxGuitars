using MelodiaxGuitarsAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class OrderProductDto
    {
        public OrderDto Order { get; set; } 
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
