using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public PaymentMethod PaymentMethod { get; set; }


        public decimal Paid { get; set; }

        public decimal Discount { get; set; }


        public virtual ICollection<PurchaseBackDetail> PurchaseBackDetails { get; set; }

        [NotMapped]
        public decimal Total => PurchaseBackDetails.Sum(s=>s.Total);




    }
}
