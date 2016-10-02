using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class SaleBack : Transaction
    {
        public SaleBack()
        {
            Details = new List<SaleBackDetail>();
        }
        

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }


        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int PointId { get; set; }

        public virtual Point Point { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


        public decimal Paid { get; set; }

        public decimal Discount { get; set; }


        public virtual ICollection<SaleBackDetail> Details { get; set; }

        [NotMapped]
        public decimal Total => Details.Sum(s=>s.Total);




    }
}
