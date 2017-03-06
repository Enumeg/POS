namespace POS.Domain.Entities
{
    public class CustomerInstallment : Installment
    {
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
