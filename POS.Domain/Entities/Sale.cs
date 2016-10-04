using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Sale : Transaction
    {
        public Sale()
        {
            SaleDetails = new List<SaleDetail>();
            CustomerInstallments = new List<CustomerInstallment>();
            SaleBacks = new List<SaleBack>();
        }             
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public virtual ICollection<SaleBack> SaleBacks { get; set; }
        public virtual ICollection<CustomerInstallment> CustomerInstallments { get; set; }
        [NotMapped]
        public decimal Total => SaleDetails.Sum(s => s.Total);
    }
}
