using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class OrderProduct
    {
        public int OrderId {  get; set; }
        [NotNull]
        public Order Order { get; set; } = new Order();
        public int ProductId { get; set; }
        [NotNull]
        public Product Product { get; set; } = new Product();
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
