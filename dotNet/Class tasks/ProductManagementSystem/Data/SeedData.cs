using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementSystem.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles
            string[] roleNames = { "Admin", "Manager", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user
            var adminEmail = "admin@productmanagement.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Manager", "User" });
                }
            }

            // Seed sample products with JSON data
            if (!context.Products.Any())
            {
                var products = new[]
                {
                    new Product
                    {
                        Name = "Laptop",
                        Price = 999.99m,
                        StockQuantity = 50,
                        Description = "High-performance laptop",
                        Category = "Electronics",
                        Specifications = "{\"color\": \"Silver\", \"weight\": \"1.5kg\", \"processor\": \"Intel i7\", \"ram\": \"16GB\", \"storage\": \"512GB SSD\", \"features\": [\"Backlit Keyboard\", \"Fingerprint Reader\"]}",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Smartphone",
                        Price = 699.99m,
                        StockQuantity = 100,
                        Description = "Latest smartphone",
                        Category = "Electronics",
                        Specifications = "{\"color\": \"Black\", \"weight\": \"0.2kg\", \"screen\": \"6.1 inch\", \"storage\": \"128GB\", \"camera\": \"12MP\", \"features\": [\"Face ID\", \"Wireless Charging\"]}",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Product
                    {
                        Name = "Office Chair",
                        Price = 199.99m,
                        StockQuantity = 30,
                        Description = "Ergonomic office chair",
                        Category = "Furniture",
                        Specifications = "{\"color\": \"Black\", \"weight\": \"15kg\", \"material\": \"Mesh\", \"adjustable\": true, \"features\": [\"Lumbar Support\", \"Height Adjustable\"]}",
                        CreatedDate = DateTime.UtcNow
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}