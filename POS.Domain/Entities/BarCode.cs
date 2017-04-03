using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class BarCode
    {
        public BarCode()
        {
            SaleDetails = new List<SaleDetailBarcode>();
            SaleBackDetails= new List<SaleBackDetail>();
            PurchaseDetails = new List<PurchaseDetailBarcode>();
            PurchaseBackDetails = new List<PurchaseBackDetailBarcode>();
        }
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<SaleDetailBarcode> SaleDetails { get; set; }
        public virtual ICollection<SaleBackDetail> SaleBackDetails { get; set; }
        public virtual ICollection<PurchaseDetailBarcode> PurchaseDetails { get; set; }
        public virtual ICollection<PurchaseBackDetailBarcode> PurchaseBackDetails { get; set; }

    }
}
