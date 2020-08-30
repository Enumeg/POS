using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Paid { get; set; }
        public decimal Discount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public TransactionType TransactionType { get; set; }
        public int PersonId { get; set; }
        public int PointId { get; set; }
        public int ShiftId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Point Point { get; set; }
        public virtual Shift Shift { get; set; }    
        public virtual BankTransaction BankTransaction { get; set; }
        public virtual ICollection<TransactionDetail> Details { get; set; }= new List<TransactionDetail>();
        public virtual ICollection<Transaction> TransactionBacks { get; set; }= new List<Transaction>();
        public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();
        public virtual ICollection<Cheque> Cheques { get; set; } = new List<Cheque>();
        [NotMapped]
        public decimal Total => Details.Sum(s => s.Total);
    }
}
