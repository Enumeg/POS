using System.Collections.Generic;

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
