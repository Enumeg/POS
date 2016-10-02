using System;

namespace POS.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int ShiftId { get; set; }

        public virtual Shift Shift { get; set; }


    }
}
