using System;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public LoanType LoanType { get; set; }



    }
}
