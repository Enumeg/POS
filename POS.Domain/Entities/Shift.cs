using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class Shift : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Balance { get; set; }
        public bool IsLast { get; set; }
        public bool IsClosing { get; set; }
        public string UserId { get; set; }
        public int? MachineId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Machine Machine { get; set; }

        [NotMapped]
        public bool IsClosed => EndDate.HasValue;
    }
}