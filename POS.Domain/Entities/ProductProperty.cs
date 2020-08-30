using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
  public  class ProductProperty
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Property Property { get; set; }
        public string Value { get; set; }
    }
}
