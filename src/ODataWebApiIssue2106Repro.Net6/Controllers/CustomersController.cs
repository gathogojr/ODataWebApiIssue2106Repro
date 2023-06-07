using System.Linq;
using Microsoft.AspNet.OData;
using ReproNS.Data;
using ReproNS.Shared.Models;

namespace ReproNS.Controllers
{
    public class CustomersController : ODataController
    {
        private readonly ReproDbContext _db;

        public CustomersController(ReproDbContext db)
        {
            _db = db;
        }

        [EnableQuery(PageSize = 2, MaxExpansionDepth = 4)]
        public IQueryable<Customer> Get()
        {
            return _db.Customers;
        }
    }
}
