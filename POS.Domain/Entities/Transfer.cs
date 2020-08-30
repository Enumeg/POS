using System.Collections.Generic;

namespace POS.Domain.Entities
{

   public class Transfer : EntityBase
    {
        public int FromPointId { get; set; }
        public virtual Point FromPoint { get; set; }
        public int ToPointId { get; set; }
        public virtual Point ToPoint { get; set; }
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }= new List<TransferDetail>();
    }

}
