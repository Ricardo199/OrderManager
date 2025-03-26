using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderManager
{

    public class Connection : DbContext
    {
        private static readonly string connectionString =
            @"Server=localhost\SQLEXPRESS; Database=OMS; Trusted_Connection=True; Encrypt=False";

        public DbSet<Product> products { get; set; }

        public DbSet<Shopper> shoppers { get; set; }

        public DbSet<Basket> baskets { get; set; }

        public DbSet<BasketItem> bitms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product"); // Explicitly map the entity to the table

                // Define the primary key
                entity.HasKey(p => p.IdProduct);

                // Configure properties (optional for additional constraints)
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(25); // Matches VARCHAR(25)

                entity.Property(p => p.Description)
                    .HasMaxLength(100); // Matches VARCHAR(100)

                entity.Property(p => p.Price)
                    .HasPrecision(6, 2); // Matches DECIMAL(6,2)
            });

            // Configure the Shopper entity
            modelBuilder.Entity<Shopper>(entity =>
            {
                entity.ToTable("Shopper");

                // Define the primary key
                entity.HasKey(s => s.IdShopper);

                // Configure properties
                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(25); // Matches VARCHAR(25)

                entity.Property(s => s.FirstName)
                    .HasMaxLength(15); // Matches VARCHAR(15)

                entity.Property(s => s.LastName)
                    .HasMaxLength(20); // Matches VARCHAR(20)

                entity.Property(s => s.Address)
                    .HasMaxLength(40); // Matches VARCHAR(40)

                entity.Property(s => s.City)
                    .HasMaxLength(20); // Matches VARCHAR(20)

                entity.Property(s => s.StateProvince)
                    .HasMaxLength(20); // Matches VARCHAR(20)

                entity.Property(s => s.Country)
                    .HasMaxLength(20); // Matches VARCHAR(20)

                entity.Property(s => s.ZipCode)
                    .HasMaxLength(15); // Matches VARCHAR(15)
            });

            // Configure the Basket entity
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("Basket");

                // Define the primary key
                entity.HasKey(b => b.IdBasket);

                // Configure properties
                entity.Property(b => b.Quantity)
                    .HasColumnType("TINYINT"); // Matches TINYINT type

                entity.Property(b => b.SubTotal)
                    .HasPrecision(7, 2); // Matches DECIMAL(7,2)

                entity.Property(b => b.OrderDate)
                    .IsRequired(); // Matches NOT NULL constraint

                // Configure the foreign key constraint
                entity.HasOne<Shopper>() // Reference to Shopper entity
                    .WithMany() // If no navigation property exists in Shopper
                    .HasForeignKey(b => b.IdShopper)
                    .HasConstraintName("bskt_idshopper_fk");
            });

            // Configure the BasketItem entity
            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.ToTable("BasketItem");

                // Define the primary key
                entity.HasKey(bi => bi.IdBasketItem);

                // Configure properties
                entity.Property(bi => bi.Quantity)
                    .HasColumnType("TINYINT"); // Matches TINYINT type

                // Configure foreign key to Basket
                entity.HasOne<Basket>() // Reference to Basket entity
                    .WithMany() // Assuming no navigation property in Basket
                    .HasForeignKey(bi => bi.IdBasket)
                    .HasConstraintName("bsktitem_bsktid_fk");

                // Configure foreign key to Product
                entity.HasOne<Product>() // Reference to Product entity
                    .WithMany() // Assuming no navigation property in Product
                    .HasForeignKey(bi => bi.IdProduct)
                    .HasConstraintName("bsktitem_idprod_fk");
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
