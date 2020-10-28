using System.Linq;
using Microsoft.AspNet.OData;
using ReproNS.Ef6.Data;
using ReproNS.Shared.Models;

namespace ReproNS.Ef6.Controllers
{
    public class CustomersController : ODataController
    {
        private readonly ReproEf6DbContext _db;

        public CustomersController(ReproEf6DbContext db)
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
