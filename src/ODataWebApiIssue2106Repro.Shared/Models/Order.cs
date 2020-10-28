using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;

namespace ReproNS.Shared.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Contained]
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
