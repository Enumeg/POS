using System.ComponentModel.DataAnnotations;

namespace POS.Domain.Entities
{
  public  class ProductProperty
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        public int PropertyId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Property Property { get; set; }
        public string Value { get; set; }
    }
}
