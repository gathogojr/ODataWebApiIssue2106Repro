using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Builder;

namespace ReproNS.Shared.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Contained]
        public Collection<Product> Products { get; set; }
    }
}
