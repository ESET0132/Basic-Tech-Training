using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models.Entities;

namespace ProductManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Product entity
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Description).HasMaxLength(500);

                // Configure JSON column for Specifications
                entity.Property(p => p.Specifications)
                    .HasColumnType("nvarchar(max)");
            });
        }
    }
}