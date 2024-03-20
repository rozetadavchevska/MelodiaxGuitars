using MelodiaxGuitarsAPI.Models;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class BrandDto
    {
        public required string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
