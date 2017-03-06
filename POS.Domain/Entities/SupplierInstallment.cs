namespace POS.Domain.Entities
{
    public class SupplierInstallment : Installment
    {
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
