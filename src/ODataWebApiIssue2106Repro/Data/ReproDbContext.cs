using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReproNS.Shared.Models;

namespace ReproNS.Data
{
    public class ReproDbContext : DbContext
    {
        public ReproDbContext(DbContextOptions<ReproDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(@Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
            
            optionsBuilder.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(d => d.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(d => d.Price).HasColumnType("decimal(18,2)");

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
