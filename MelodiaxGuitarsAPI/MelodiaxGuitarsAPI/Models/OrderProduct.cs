using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class OrderProduct
    {
        [Key]
        public required string Id { get; set; }
        public required string OrderId {  get; set; }
        public Order? Order { get; set; }
        public required string ProductId { get; set; }
        public Product? Product { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
