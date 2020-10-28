using System.Linq;
using Microsoft.AspNet.OData;
using ReproNS.Ef6.Data;
using ReproNS.Shared.Models;

namespace ReproNS.Ef6.Controllers
{
    public class OrdersController
    {
        private readonly ReproEf6DbContext _db;

        public OrdersController(ReproEf6DbContext db)
        {
            _db = db;
        }

        [EnableQuery(PageSize = 2, MaxExpansionDepth = 4)]
        public IQueryable<Order> Get()
        {
            return _db.Orders;
        }
    }
}
