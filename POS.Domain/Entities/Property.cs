using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<ProductProperty> Products { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
