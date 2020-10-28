using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using ReproNS.Ef6.Data;
using ReproNS.Shared.Models;

namespace ReproNS.Ef6
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddScoped(_ => new ReproEf6DbContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().OrderBy().Count().Expand().MaxTop(null);
                routeBuilder.MapODataServiceRoute(
                    routeName: "odata",
                    routePrefix: "odata",
                    model: GetEdmModel());
            });

            Seed(app);
        }

        private static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Order>("Orders");
            modelBuilder.EntitySet<Customer>("Customers");

            return modelBuilder.GetEdmModel();
        }

        private static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<ReproEf6DbContext>();

                if (!db.Orders.Any())
                {
                    DbContextTransaction transaction = null;

                    try
                    {
                        transaction = db.Database.BeginTransaction();

                        // Categories
                        db.Categories.AddRange(new[]
                        {
                            new Category { Id = 1, Name = "Food" },
                            new Category { Id = 2, Name = "Non-Food" }
                        });

                        // Products
                        db.Products.AddRange(new[]
                        {
                            new Product { Id = 1, CategoryId = 1, Name = "Sugar", Price = 6M },
                            new Product { Id = 2, CategoryId = 1, Name = "Coffee", Price = 6M },
                            new Product { Id = 3, CategoryId = 2, Name = "Paper", Price = 14M },
                            new Product { Id = 4, CategoryId = 2, Name = "Pencil", Price = 14M }
                        });

                        // Customers
                        db.Customers.AddRange(new[]
                        {
                            new Customer { Id = 1, Name = "Joe" },
                            new Customer { Id = 2, Name = "Sue" },
                            new Customer { Id = 3, Name = "Sue" },
                            new Customer { Id = 4, Name = "Luc" }
                        });

                        // Orders
                        db.Orders.AddRange(new[]
                        {
                            new Order { Id = 1, CustomerId = 1 },
                            new Order { Id = 2, CustomerId = 3 },
                            new Order { Id = 3, CustomerId = 2 },
                            new Order { Id = 4, CustomerId = 3 }
                        });

                        // Order Items
                        db.OrderItems.AddRange(new[]
                            {
                            new OrderItem { Id = 1, OrderId = 1, ProductId = 3, Price = 14M, Quantity = 1 },
                            new OrderItem { Id = 2, OrderId = 1, ProductId = 1, Price = 6M, Quantity = 1 },
                            new OrderItem { Id = 3, OrderId = 1, ProductId = 2, Price = 6M, Quantity = 1 },
                            new OrderItem { Id = 4, OrderId = 2, ProductId = 1, Price = 6M, Quantity = 1 },
                            new OrderItem { Id = 5, OrderId = 2, ProductId = 3, Price = 14M, Quantity = 1 },
                            new OrderItem { Id = 6, OrderId = 3, ProductId = 2, Price = 6M, Quantity = 1 },
                            new OrderItem { Id = 7, OrderId = 3, ProductId = 3, Price = 14M, Quantity = 1 },
                            new OrderItem { Id = 8, OrderId = 4, ProductId = 3, Price = 14M, Quantity = 1 }
                        });

                        // Addresses
                        db.CustomerAddresses.AddRange(new[]
                        {
                            new CustomerAddress { Id = 1, AddressLine = "13, Cerritos, Los Cerritos Center", City = "Cerritos", Country = "USA", CustomerId = 1 },
                            new CustomerAddress { Id = 2, AddressLine = "7, Los Angeles, Westfield Century City", City = "Los Angeles", Country = "USA", CustomerId = 1 },
                            new CustomerAddress { Id = 3, AddressLine = "Evert van de Beekstraat 354, 1118 CZ Schiphol", City = "Amsterdam", Country = "Netherlands", CustomerId = 3 },
                            new CustomerAddress { Id = 4, AddressLine = "23, Palo Alto, Stanford Shopping Center", City = "Palo Alto", Country = "USA", CustomerId = 4 }
                        });

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        db.Database.Connection.Close();
                    }
                }
            }
        }
    }
}
