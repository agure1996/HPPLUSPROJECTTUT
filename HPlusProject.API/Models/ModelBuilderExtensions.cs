using Microsoft.EntityFrameworkCore;

namespace HPlusProject.API.Models
{
    public static class ModelBuilderExtensions
    {

        static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Sports" },
            new Category { Id = 3, Name = "Home Appliances" }
        };

        static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Sku = "ELC123", Name = "Smartphone", Description = "Latest smartphone with 5G", Price = 699.99M, IsAvailable = true, CategoryId = 1 },
            new Product { Id = 2, Sku = "ELC124", Name = "Laptop", Description = "High-performance laptop", Price = 1199.99M, IsAvailable = true, CategoryId = 1 },
            new Product { Id = 3, Sku = "ELC125", Name = "Wireless Earbuds", Description = "Noise-cancelling wireless earbuds", Price = 199.99M, IsAvailable = false, CategoryId = 1 },
            new Product { Id = 4, Sku = "SPT123", Name = "Basketball", Description = "Indoor/outdoor basketball", Price = 29.99M, IsAvailable = true, CategoryId = 2 },
            new Product { Id = 5, Sku = "SPT124", Name = "Tennis Racket", Description = "Professional tennis racket", Price = 89.99M, IsAvailable = true, CategoryId = 2 },
            new Product { Id = 6, Sku = "SPT125", Name = "Running Shoes", Description = "Lightweight running shoes", Price = 129.99M, IsAvailable = false, CategoryId = 2 },
            new Product { Id = 7, Sku = "HAP123", Name = "Vacuum Cleaner", Description = "High-suction vacuum cleaner", Price = 249.99M, IsAvailable = true, CategoryId = 3 },
            new Product { Id = 8, Sku = "HAP124", Name = "Air Fryer", Description = "Healthy cooking air fryer", Price = 99.99M, IsAvailable = false, CategoryId = 3 },
            new Product { Id = 9, Sku = "HAP125", Name = "Blender", Description = "High-speed blender", Price = 59.99M, IsAvailable = true, CategoryId = 3 },
            new Product { Id = 10, Sku = "HAP126", Name = "Microwave Oven", Description = "Compact microwave oven", Price = 149.99M, IsAvailable = true, CategoryId = 3 },
            new Product { Id = 11, Sku = "OUT123", Name = "Gaming Console", Description = "Out-of-stock gaming console", Price = 499.99M, IsAvailable = false, CategoryId = 1 },
            new Product { Id = 12, Sku = "APP456", Name = "Smartwatch", Description = "Unavailable smartwatch model", Price = 199.99M, IsAvailable = true, CategoryId = 1 },
            new Product { Id = 13, Sku = "HAP789", Name = "Dishwasher", Description = "Discontinued dishwasher model", Price = 299.99M, IsAvailable = false, CategoryId = 3 }
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
