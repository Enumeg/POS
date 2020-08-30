using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class BarCode : EntityBase
    {
        public string Barcode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<TransactionDetailBarcode> Details { get; set; } = new HashSet<TransactionDetailBarcode>();
    }
}
