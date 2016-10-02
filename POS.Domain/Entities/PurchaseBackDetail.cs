namespace POS.Domain.Entities
{
    public class PurchaseBackDetail : TransactionDetail
    {
        public int PurchaseBackId { get; set; }
        public virtual PurchaseBack PurchaseBack { get; set; }
    }
}
