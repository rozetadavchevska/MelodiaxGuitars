namespace MelodiaxGuitarsAPI.DTOs
{
    public class BrandDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
