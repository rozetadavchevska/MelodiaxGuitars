using MelodiaxGuitarsAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public BrandDto Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Hand { get; set; }
        public string BodyShape { get; set; }
        public string Color { get; set; }
        public string Top { get; set; }
        public string SidesAndBack { get; set; }
        public string Neck { get; set; }
        public string Nut { get; set; }
        public string Fingerboard { get; set; }
        public string Strings { get; set; }
        public string Tuners { get; set; }
        public string Bridge { get; set; }
        public string Controls { get; set; }
        public string Pickups { get; set; }
        public string PickupSwitch { get; set; }
        public bool Cutaway { get; set; }
        public string Pickguard { get; set; }
        public string Case { get; set; }
        public string ScaleLength { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string Weight { get; set; }
        public CategoryDto Category { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<OrderProductDto> OrderProducts { get; set; }
    }
}
