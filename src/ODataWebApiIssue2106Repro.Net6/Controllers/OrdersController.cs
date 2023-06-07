using System.Linq;
using Microsoft.AspNet.OData;
using ReproNS.Data;
using ReproNS.Shared.Models;

namespace ReproNS.Controllers
{
    public class OrdersController
    {
        private readonly ReproDbContext _db;

        public OrdersController(ReproDbContext db)
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
