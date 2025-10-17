using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.Entities;

namespace RestaurantApi.Models.DbContext
{
    public class RestaurantDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Menu>()
                .Property(m => m.Price)
                .HasPrecision(18, 2); 

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

           
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .HasMaxLength(200);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Menu>()
                .Property(m => m.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<Menu>()
                .Property(m => m.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Menu>()
                .Property(m => m.Category)
                .HasMaxLength(50);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerName)
                .HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerPhone)
                .HasMaxLength(20);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerEmail)
                .HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasMaxLength(20);

           
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Menus)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Orders)
                .WithOne(o => o.Restaurant)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); 

           
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.OrderItems)
                .WithOne(oi => oi.Menu)
                .HasForeignKey(oi => oi.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}