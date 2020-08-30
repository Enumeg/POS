using System;
using System.Collections.Generic;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Income : EntityBase
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int PersonId { get; set; }
        public IncomeAccountType AccountType { get; set; }
        public string Description { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Installment> Installments { get; set; } = new List<Installment>();
    }

   
}
