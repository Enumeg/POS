using POS.Domain.Enums;
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
            OutTransfares = new List<Transfer>();
            InTransfares = new List<Transfer>();
            Damageds = new List<Damaged>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public PointType PointType { get; set; } 
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<SaleBack> SaleBacks { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseBack> PurchaseBacks { get; set; }
        public virtual ICollection<Transfer> InTransfares { get; set; }
        public virtual ICollection<Transfer> OutTransfares { get; set; }
        public virtual ICollection<Damaged> Damageds { get; set; }

    }
}
