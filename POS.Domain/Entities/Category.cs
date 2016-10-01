using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
            Units = new List<Unit>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Unit> Units { get; set; }
    }
}
