using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Brand description is required")]
        public string Description { get; set; } = string.Empty;
        public ICollection<Product>? Products { get; set; }

    }
}
