using Microsoft.EntityFrameworkCore;
using ShopsRUs.Domain.Invoices;
using ShopsRUs.Domain.Products;
using ShopsRUs.Domain.Customers;
using System;
using ShopsRUs.Domain.Orders;
using ShopsRUs.Domain.Discounts;

namespace ShopsRUs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}