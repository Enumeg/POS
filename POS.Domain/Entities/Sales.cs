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
    public class Sales : Transaction
    {
        public Sales()
        {
            SalesDetails = new List<SalesDetail>();
        }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int PointId { get; set; }

        public virtual Point Point { get; set; }

        public PaymentMethod PaymentMethod { get; set; }


        public decimal Paid { get; set; }

        public decimal Discount { get; set; }


        public virtual ICollection<SalesDetail> SalesDetails { get; set; }

        [NotMapped]
        public decimal Total => SalesDetails.Sum(s=>s.Total);




    }
}
