using System;
namespace POS.Domain.Entities
{
   public class Cheque
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public DateTime DueDate { get; set; }

        public string Number { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }

    }
    public class CustomerCheque : Cheque
    {
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
    public class SupplierCheque : Cheque
    {
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
