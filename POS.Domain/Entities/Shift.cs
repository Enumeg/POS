using POS.Domain.Enums;
using System;

namespace POS.Domain.Entities
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Balance { get; set; }
        public bool IsClosed { get; set; }
        public string UserId { get; set; }
        public int? MachineId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Machine Machine { get; set; }
    }
}