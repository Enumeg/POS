using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class TransactionDetail : EntityBase
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<TransactionDetailBarcode> Barcodes { get; set; }= new List<TransactionDetailBarcode>();
        [NotMapped]
        public decimal Total => Amount * Price;

    }

}
