﻿using Microsoft.EntityFrameworkCore;

namespace HPlusProject.API.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Seed();
        }

            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
        
        
        
    }
}
