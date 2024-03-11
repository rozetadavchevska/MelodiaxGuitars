using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MelodiaxGuitarsAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [NotNull]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product description is required")]
        [NotNull]
        public string Description { get; set; } = string.Empty;
        public int BrandId {  get; set; }
        public Brand? Brand { get; set; } 
        [AllowNull]
        public string Model { get; set; }
        [AllowNull]
        public string Type { get; set; }
        [AllowNull]
        public string Hand { get; set; }
        [AllowNull]
        public string BodyShape { get; set; }
        [AllowNull]
        public string Color { get; set; }
        [AllowNull]
        public string Top { get; set; }
        [AllowNull]
        public string SidesAndBack { get; set; }
        [AllowNull]
        public string Neck { get; set; }
        [AllowNull]
        public string Nut { get; set; }
        [AllowNull]
        public string Fingerboard { get; set;}
        [AllowNull]
        public string Strings { get; set; }
        [AllowNull]
        public string Tuners { get; set; }
        [AllowNull]
        public string Bridge { get; set; }
        [AllowNull]
        public string Controls { get; set; }
        [AllowNull]
        public string Pickups { get; set; }
        [AllowNull]
        public string PickupSwitch {  get; set; }
        [AllowNull]
        public bool Cutaway { get; set; }
        [AllowNull]
        public string Pickguard { get; set; }
        [AllowNull]
        public string Case {  get; set; }
        [AllowNull]
        public string ScaleLength { get; set; }
        [AllowNull]
        public string Width { get; set; }
        [AllowNull]
        public string Depth { get; set; }
        [AllowNull]
        public string Weight { get; set; }
        public int CategoryId {  get; set; }
        public Category? Category { get; set; }
        [AllowNull]
        public string ImageUrl { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
