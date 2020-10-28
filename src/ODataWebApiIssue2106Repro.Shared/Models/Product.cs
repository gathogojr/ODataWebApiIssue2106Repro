using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;

namespace ReproNS.Shared.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Contained]
        public Collection<OrderItem> OrderItems { get; set; }
    }
}
