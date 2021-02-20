using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Unit : BiLanguageNameEntity
    {
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
