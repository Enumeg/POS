using System;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Income
    {
        public Income()
        {
            Installments = new List<Installment>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Installment> Installments { get; set; }
    }
}
