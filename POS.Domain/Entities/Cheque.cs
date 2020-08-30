using System;
namespace POS.Domain.Entities
{
    public class Cheque : EntityBase
    {

        public decimal Value { get; set; }

        public DateTime DueDate { get; set; }

        public string Number { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int BankAccountId { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        
        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }

    }   
}
