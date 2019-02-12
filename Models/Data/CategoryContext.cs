using Core.Entity;
using Core.Entity.Cart;
using Core.Entity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>(ConfigureCart);
            builder.Entity<Category>(ConfigureCategory);
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<OrderItem>(ConfigureOrderItem);
            builder.Entity<Address>(ConfigureAddress);
            builder.Entity<CategoryOrdered>(ConfigurateCategoryOrdered);
        }

        private void ConfigurateCategoryOrdered(EntityTypeBuilder<CategoryOrdered> builder)
        {
            builder.Property(cio => cio.ProductName)
                .HasMaxLength(50)
                .IsRequired();
        }

        private void ConfigureAddress(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Street)
                .HasMaxLength(180)
                .IsRequired();

            builder.Property(a => a.Country)
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        }

        private void ConfigureCart(EntityTypeBuilder<Cart> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Cart.Items));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.HasOne(ci => ci.Category)
                .WithMany()
                .HasForeignKey(ci => ci.CategoryId);
        }

        
        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CategoryType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("category_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(o => o.ShippingAddress);
        }

        private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.Ordered);
        }
    }
}

