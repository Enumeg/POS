using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Category : BiLanguageNameEntity
    {
        public Category()
        {
            Products = new List<Product>();
            Units = new List<Unit>();
            Properties = new List<Property>();
        }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
        public virtual ICollection<Property> Properties { get; set; }


    }
}
