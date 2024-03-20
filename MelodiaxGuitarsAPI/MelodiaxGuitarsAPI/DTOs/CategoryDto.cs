using MelodiaxGuitarsAPI.Models;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class CategoryDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
