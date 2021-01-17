using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PointOfSaleApp.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PointOfSaleApp.Data.Entities
{
    public class PointOfSaleDbContext : DbContext
    {
        public PointOfSaleDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferCategory> OfferCategories { get; set; }
        public DbSet<OneOffBill> OneOffBills { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceBill> ServiceBills { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionBill> SubscriptionBills { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //todo: Seed data
        }
    }

    public class PointOfSaleDbContextFactory : IDesignTimeDbContextFactory<PointOfSaleDbContext>
    {
        public PointOfSaleDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            configuration
                .Providers
                .First()
                .TryGet("connectionStrings:add:PointOfSaleDb:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<PointOfSaleDbContext>().UseSqlServer(connectionString).Options;

            return new PointOfSaleDbContext(options);
        }
    }
}
