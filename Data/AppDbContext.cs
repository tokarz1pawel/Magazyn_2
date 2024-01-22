using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Magazyn.Models;
using System.Configuration;

namespace Magazyn.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().HasData(
                new Products()
                {
                    Id = -1,
                    Name = "Koszulka",
                    Description = "Biała",
                    Price = 250.00,
                    StockQuantity = 190
                },
                new Products()
                {
                    Id = -2,
                    Name = "Myszka",
                    Description = "Logitech G PRO",
                    Price = 450.00,
                    StockQuantity = 86
                },
                new Products()
                {
                    Id = -3,
                    Name = "Klawiatura",
                    Description = "Logitech",
                    Price = 780.00,
                    StockQuantity = 30
                },
                new Products()
                {
                    Id = -4,
                    Name = "Monitor",
                    Description = "Dell",
                    Price = 1200.00,
                    StockQuantity = 490
                },
                new Products()
                {
                    Id = -5,
                    Name = "Karta Graficzna",
                    Description = "RTX 4070TI",
                    Price = 10000.00,
                    StockQuantity = 7
                },
                new Products()
                {
                    Id = -6,
                    Name = "Krzesło",
                    Description = "Gamingowe",
                    Price = 750.00,
                    StockQuantity = 34
                },
                new Products()
                {
                    Id = -7,
                    Name = "Łyżka",
                    Description = "Zwykła łyżka",
                    Price = 15.00,
                    StockQuantity = 108
                },
                new Products()
                {
                    Id = -8,
                    Name = "Dywan",
                    Description = "Szary",
                    Price = 320.00,
                    StockQuantity = 48
                },
                new Products()
                {
                    Id = -9,
                    Name = "Nóż",
                    Description = "Srebrny",
                    Price = 14.00,
                    StockQuantity = 0
                }
                );

            modelBuilder.Entity<Orders>()
                .Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Orders>()
                .Property(e => e.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Products>()
                .Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Products>()
                .Property(e => e.UpdatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}