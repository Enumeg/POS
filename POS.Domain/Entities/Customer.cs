using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Customer : Person
    {
        public Customer()
        {
            Sales = new List<Sale>();
            SaleBacks = new List<SaleBack>();
        }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<SaleBack> SaleBacks { get; set; }

    }
}
