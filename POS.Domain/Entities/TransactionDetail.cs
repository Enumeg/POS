using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class TransactionDetail
    {
        public TransactionDetail()
        {
            Barcodes = new List<TransactionDetailBarcode>();
        }
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public decimal Total => Amount * Price;

        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<TransactionDetailBarcode> Barcodes { get; set; }
    }

}
