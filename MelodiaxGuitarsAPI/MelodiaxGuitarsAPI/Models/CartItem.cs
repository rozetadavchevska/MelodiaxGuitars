using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
