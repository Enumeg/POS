using POS.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
   public class Property : BiLanguageNameEntity
    {
        public virtual ICollection<ProductProperty> Products { get; set; } = new List<ProductProperty>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        [NotMapped]
        public List<PropertyValue> Values { get; set; }= new List<PropertyValue>();
    }
}
