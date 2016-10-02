namespace POS.Domain.Entities
{
    public class PurchaseDetail : TransactionDetail
    {
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
