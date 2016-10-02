using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Customer : Person
    {
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<SaleBack> Backs { get; set; }
    }
}
