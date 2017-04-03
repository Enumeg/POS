using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Properties = new List<ProductProperty>();
            Barcodes = new List<BarCode>();
            SaleDetails = new List<SaleDetail>();
            SaleBackDetails = new List<SaleBackDetail>();
            PurchaseDetails = new List<PurchaseDetail>();
            PurchaseBackDetails = new List<PurchaseBackDetail>();
            TransfareDetails = new List<TransferDetail>();
            DamagedDetails = new List<DamagedDetail>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public  string BarCode { get; set; }
        public decimal SalePrice { get; set; }   
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductProperty> Properties { get; set; }
        public virtual ICollection<BarCode> Barcodes { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
        public virtual ICollection<SaleBackDetail> SaleBackDetails { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual ICollection<PurchaseBackDetail> PurchaseBackDetails { get; set; }
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }
        public virtual ICollection<DamagedDetail> DamagedDetails { get; set; }


    }
}
