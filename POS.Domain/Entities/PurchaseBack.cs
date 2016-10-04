using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class PurchaseBack : Transaction
    {
        public PurchaseBack()
        {
            PurchaseBackDetails = new List<PurchaseBackDetail>();
        }       
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<PurchaseBackDetail> PurchaseBackDetails { get; set; }
        [NotMapped]
        public decimal Total => PurchaseBackDetails.Sum(s=>s.Total);




    }
}
