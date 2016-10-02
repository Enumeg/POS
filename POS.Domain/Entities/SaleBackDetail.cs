namespace POS.Domain.Entities
{
    public class SaleBackDetail : TransactionDetail
    {
        public int SaleBackId { get; set; }
        public virtual SaleBack SaleBack { get; set; }
    }
}
