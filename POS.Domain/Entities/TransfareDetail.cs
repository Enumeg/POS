namespace POS.Domain.Entities
{
    public class TransfareDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
    }
}