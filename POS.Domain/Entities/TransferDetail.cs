namespace POS.Domain.Entities
{
    public class TransferDetail : ProductDetail
    {
        public int TransferId { get; set; }
        public virtual Transfer Transfer { get; set; }

    }
}