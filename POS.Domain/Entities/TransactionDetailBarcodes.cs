using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class TransactionDetailBarcode
    {
        public int Id { get; set; }
        public int BarCodeId { get; set; }
        public virtual BarCode BarCode { get; set; }
    }
    public class SaleDetailBarcode : TransactionDetailBarcode
    {
        public int SaleDetailId { get; set; }
        public virtual SaleDetail SaleDetail { get; set; }
    }
    public class SaleBackDetailBarcode : TransactionDetailBarcode
    {
        public int SaleBackDetailId { get; set; }
        public virtual SaleBackDetail SaleBackDetail { get; set; }
    }
    public class PurchaseDetailBarcode : TransactionDetailBarcode
    {
        public int PurchaseDetailId { get; set; }
        public virtual PurchaseDetail PurchaseDetail { get; set; }
    }
    public class PurchaseBackDetailBarcode : TransactionDetailBarcode
    {
        public int PurchaseBackDetailId { get; set; }
        public virtual PurchaseBackDetail PurchaseBackDetail { get; set; }
    }

}
