﻿namespace MelodiaxGuitarsAPI.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User {  get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
