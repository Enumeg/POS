using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Product : BiLanguageNameEntity
    { 
        public string Barcode { get; set; }
        public decimal SalePrice { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductProperty> Properties { get; set; } = new List<ProductProperty>();
        public virtual ICollection<BarCode> Barcodes { get; set; } = new List<BarCode>();
        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }= new List<TransferDetail>();
        public virtual ICollection<DamagedDetail> DamagedDetails { get; set; } = new List<DamagedDetail>();
        public virtual ICollection<Stock> Stores { get; set; }= new List<Stock>();

    }
}
