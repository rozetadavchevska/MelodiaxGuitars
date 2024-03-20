using MelodiaxGuitarsAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; } 
        public required string BrandId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Hand { get; set; } = string.Empty;
        public string BodyShape { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Top { get; set; } = string.Empty;
        public string SidesAndBack { get; set; } = string.Empty;
        public string Neck { get; set; } = string.Empty;
        public string Nut { get; set; } = string.Empty;
        public string Fingerboard { get; set; } = string.Empty;
        public string Strings { get; set; } = string.Empty;
        public string Tuners { get; set; } = string.Empty;
        public string Bridge { get; set; } = string.Empty;
        public string Controls { get; set; } = string.Empty;
        public string Pickups { get; set; } = string.Empty;
        public string PickupSwitch { get; set; } = string.Empty;
        public bool Cutaway { get; set; }
        public string Pickguard { get; set; } = string.Empty;
        public string Case { get; set; } = string.Empty;
        public string ScaleLength { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
        public string Depth { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public required string CategoryId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
