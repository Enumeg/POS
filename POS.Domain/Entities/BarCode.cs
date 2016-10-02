namespace POS.Domain.Entities
{
    public class BarCode
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }       
    }
}
