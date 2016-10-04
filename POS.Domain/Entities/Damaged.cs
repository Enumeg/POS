using System.Collections.Generic;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{

   public class Damaged
    {
        public Damaged()
        {
            TransfareDetails = new List<TransferDetail>();
        }
        public int Id { get; set; }

        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }
    }

}
