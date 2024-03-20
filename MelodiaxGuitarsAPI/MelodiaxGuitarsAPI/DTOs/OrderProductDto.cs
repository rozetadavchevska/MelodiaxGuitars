using MelodiaxGuitarsAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class OrderProductDto
    {
        public required string OrderId { get; set; } 
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
