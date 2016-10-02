using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Point
    {
        public Point()
        {
            Sales = new List<Sale>();
            SaleBacks = new List<SaleBack>();
            Purchases = new List<Purchase>();
            PurchaseBacks = new List<PurchaseBack>();
            OutTransfares = new List<Transfare>();
            InTransfares = new List<Transfare>();
            Damageds = new List<Damaged>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }    
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<SaleBack> SaleBacks { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseBack> PurchaseBacks { get; set; }
        public virtual ICollection<Transfare> InTransfares { get; set; }
        public virtual ICollection<Transfare> OutTransfares { get; set; }
        public virtual ICollection<Damaged> Damageds { get; set; }

    }
}
