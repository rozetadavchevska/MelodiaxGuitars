using MelodiaxGuitarsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MelodiaxGuitarsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var idProperty = entityType.FindProperty("Id");
                if (idProperty != null && idProperty.ClrType == typeof(int))
                {
                    idProperty.ValueGenerated = ValueGenerated.OnAdd;
                }
            }

            modelBuilder.Entity<OrderProduct>().HasKey(p => new
            {
                p.ProductId,
                p.OrderId
            });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(po => po.OrderProducts)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingCost)
                .HasColumnType("decimal(8, 2)"); 

            modelBuilder.Entity<Order>()
                .Property(o => o.SubtotalCost)
                .HasColumnType("decimal(8, 2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalCost)
                .HasColumnType("decimal(8, 2)");

            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(sc => sc.User)
                .HasForeignKey<ShoppingCart>(sc => sc.UserId);


           /* modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithOne(sc => sc.ShoppingCart)
                .HasForeignKey<User>(sc => sc.ShoppingCartId);*/

            base.OnModelCreating(modelBuilder);
        }

        

        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts {  get; set; }
        public DbSet<User> Users { get; set; }
    }
}
