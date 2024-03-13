using MelodiaxGuitarsAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MelodiaxGuitarsAPI.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var primaryKey = entityType.FindPrimaryKey();
                if (primaryKey != null)
                {
                    foreach (var property in primaryKey.Properties)
                    {
                        property.ValueGenerated = ValueGenerated.OnAdd;
                    }
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


            base.OnModelCreating(modelBuilder);
        }

        

        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts {  get; set; }
    }
}
