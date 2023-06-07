using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using ReproNS.Data;
using ReproNS.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReproDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddOData();

var app = builder.Build();

var modelBuilder = new ODataConventionModelBuilder();

modelBuilder.EntitySet<Order>("Orders");
modelBuilder.EntitySet<Customer>("Customers");

var model = modelBuilder.GetEdmModel();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.EnableDependencyInjection();
    endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(null);
    endpoints.MapODataRoute(routeName: "odata", routePrefix: "odata", model: model);
});

app.Run();
