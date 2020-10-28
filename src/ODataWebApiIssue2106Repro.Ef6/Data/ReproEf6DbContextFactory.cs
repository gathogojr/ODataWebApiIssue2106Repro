using System.Data.Entity.Infrastructure;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ReproNS.Ef6.Data
{
    public class ReproEf6DbContextFactory : IDbContextFactory<ReproEf6DbContext>
    {
        public ReproEf6DbContext Create()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(@Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();

            return new ReproEf6DbContext(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
