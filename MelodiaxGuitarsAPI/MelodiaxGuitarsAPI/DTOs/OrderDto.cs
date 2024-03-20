using MelodiaxGuitarsAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class OrderDto
    {
        public required string UserId { get; set; } 
        public decimal SubtotalCost { get; set; }
        public bool Shipping { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
