namespace MelodiaxGuitarsAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public decimal Subtotal { get; set; }
        public bool Shipping { get; set; }
        public decimal Total { get; set; }
    }
}
