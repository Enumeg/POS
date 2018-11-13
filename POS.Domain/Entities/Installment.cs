using System;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int IncomeId { get; set; }

        public Income Income { get; set; }

        public int? ShiftId { get; set; }

        public virtual Shift Shift { get; set; }

        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }

    }
}
