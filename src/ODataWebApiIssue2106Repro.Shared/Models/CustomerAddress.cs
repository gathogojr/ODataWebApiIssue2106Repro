using System.ComponentModel.DataAnnotations.Schema;

namespace ReproNS.Shared.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
