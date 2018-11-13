using POS.Domain.Enums;
using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Point
    {
        public Point()
        {
            Transactions = new List<Transaction>();
            OutTransfares = new List<Transfer>();
            InTransfares = new List<Transfer>();
            Damageds = new List<Damaged>();
            Stores = new List<Stock>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public PointType PointType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Transfer> InTransfares { get; set; }
        public virtual ICollection<Transfer> OutTransfares { get; set; }
        public virtual ICollection<Damaged> Damageds { get; set; }
        public virtual ICollection<Stock> Stores { get; set; }

    }
}
