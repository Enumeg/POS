using POS.Domain.Enums;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Point : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public PointType PointType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transfer> InTransfares { get; set; } = new List<Transfer>();
        public virtual ICollection<Transfer> OutTransfares { get; set; } = new List<Transfer>();
        public virtual ICollection<Damaged> Damageds { get; set; } = new List<Damaged>();
        public virtual ICollection<Stock> Stores { get; set; } = new List<Stock>();

    }
}
