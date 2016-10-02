namespace POS.Domain.Entities
{
    public class TransferDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
    }
}