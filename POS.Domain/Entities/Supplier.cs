using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Supplier : Person
    {
        public Supplier()
        {
            Purchases = new List<Purchase>();
            PurchasecBacks = new List<PurchaseBack>();
        }    
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseBack> PurchasecBacks { get; set; }

    }
}
