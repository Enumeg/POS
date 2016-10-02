using System.Collections.Generic;

namespace POS.Domain.Entities
{
   public class Property
    {
        public Property()
        {
            Products = new List<ProductProperty>();
            Categories = new List<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductProperty> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
