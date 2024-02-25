﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MelodiaxGuitarsAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [NotNull]
        public User User { get; set; } = new User();
        public ICollection<OrderProduct>? OrderProducts { get; set; }
        [Required(ErrorMessage = "Subtotal cost is required")]
        public decimal SubtotalCost { get; set; } 
        public bool Shipping { get; set; }
        [AllowNull]
        public decimal ShippingCost { get; set; }
        [Required(ErrorMessage = "Total cost is required")]
        public decimal TotalCost { get; set; }
    }
}
