using System.Collections.Generic;

namespace POS.Domain.Entities
{

   public class Transfer 
    {
        public Transfer()
        {
            TransfareDetails = new List<TransferDetail>();
        }
        public int Id { get; set; }
        public int FromPointId { get; set; }
        public virtual Point FromPoint { get; set; }
        public int ToPointId { get; set; }
        public virtual Point ToPoint { get; set; }
        public virtual ICollection<TransferDetail> TransfareDetails { get; set; }
    }

}
