using POS.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
   public class Property
    {
        public Property()
        {
            Products = new List<ProductProperty>();
            Categories = new List<Category>();
            Values = new List<PropertyValue>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductProperty> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        [NotMapped]
        public List<PropertyValue> Values { get; set; }
    }
}
