using System.Collections.Generic;

namespace POS.Domain.Entities
{
   public class Unit
    {
        public Unit()
        {
            Categories = new List<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
