using System.Collections.ObjectModel;
using Microsoft.AspNet.OData.Builder;

namespace ReproNS.Shared.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Contained]
        public Collection<CustomerAddress> Addresses { get; set; }
        public Collection<Order> Orders { get; set; }
    }
}
