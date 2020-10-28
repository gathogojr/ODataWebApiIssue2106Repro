using System.Data.Entity;
using System.IO;
using Microsoft.Extensions.Configuration;
using ReproNS.Shared.Models;

namespace ReproNS.Ef6.Data
{
    public class ReproEf6DbContext : DbContext
    {
        public ReproEf6DbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
