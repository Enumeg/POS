using System.Collections.Generic;

namespace POS.Domain.Entities
{
   public class Unit
    {
        public Unit()
        {
            Categories = new List<Category>();
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
