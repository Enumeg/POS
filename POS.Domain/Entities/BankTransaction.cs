using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Number { get; set; }      
        public int BankAccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
    public class Deposit : BankTransaction
    {
        //public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }

    }
    public class WithDrawal : BankTransaction
    {
        //public int? PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

    }
}
