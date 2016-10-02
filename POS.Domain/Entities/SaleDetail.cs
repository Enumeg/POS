namespace POS.Domain.Entities
{
    public class SaleDetail : TransactionDetail
    {
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
