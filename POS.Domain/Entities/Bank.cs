using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class Bank: EntityBase
    {
        public Bank()
        {
            BankAccounts = new HashSet<BankAccount>();
        }
        public string Name { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

    }
    public class BankAccount : EntityBase
    {
        public BankAccount()
        {
            Cheques = new List<Cheque>();
            BankTransactions = new List<BankTransaction>();
        }
        public string Name { get; set; }
        public string Number { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }    
        public virtual ICollection<Cheque> Cheques { get; set; }    
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }    

        [NotMapped]
        public string Label => Name + " - " + Number; 
    }

    public class BankTransaction : EntityBase
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Number { get; set; }
        public int BankAccountId { get; set; }
        public int TransactionId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
