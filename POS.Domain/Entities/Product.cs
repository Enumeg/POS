using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Properties = new List<ProductProperty>();
            Barcodes = new List<BarCode>();
            TransactionDetails = new List<TransactionDetail>();
            TransfareDetails = new List<TransferDetail>();
            DamagedDetails = new List<DamagedDetail>();
            Stores = new List<Stock>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public  string Barcode { get; set; }
        public decimal SalePrice { get; set; }   
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductProperty> Properties { get; set; }
        public virtual ICollection<BarCode> Barcodes { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }
        public virtual ICollection<DamagedDetail> DamagedDetails { get; set; }
        public virtual ICollection<Stock> Stores { get; set; }

    }
}
