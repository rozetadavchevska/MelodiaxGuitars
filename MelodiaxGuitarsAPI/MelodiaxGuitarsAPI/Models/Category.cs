using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [NotNull]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public ICollection<Product>? Products { get; set; }
    }
}
