using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public decimal Total => Amount * Price;
    }

    public class SaleDetail : TransactionDetail
    {
        public SaleDetail()
        {
            Barcodes = new List<SaleDetailBarcode>();
        }
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual ICollection<SaleDetailBarcode> Barcodes { get; set; }

    }
    public class SaleBackDetail : TransactionDetail
    {
        public SaleBackDetail()
        {
            Barcodes = new List<SaleBackDetailBarcode>();
        }
        public int SaleBackId { get; set; }
        public virtual SaleBack SaleBack { get; set; }
        public virtual ICollection<SaleBackDetailBarcode> Barcodes { get; set; }

    }
    public class PurchaseDetail : TransactionDetail
    {
        public PurchaseDetail()
        {
            Barcodes = new List<PurchaseDetailBarcode>();
        }
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual ICollection<PurchaseDetailBarcode> Barcodes { get; set; }

    }
    public class PurchaseBackDetail : TransactionDetail
    {
        public PurchaseBackDetail()
        {
            Barcodes = new List<PurchaseBackDetailBarcode>();
        }
        public int PurchaseBackId { get; set; }
        public virtual PurchaseBack PurchaseBack { get; set; }
        public virtual ICollection<PurchaseBackDetailBarcode> Barcodes { get; set; }

    }
}
