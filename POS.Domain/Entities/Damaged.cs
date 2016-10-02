using System.Collections.Generic;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{

   public class Damaged
    {
        public Damaged()
        {
            TransfareDetails = new List<TransfareDetail>();
        }      
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<TransfareDetail> TransfareDetails { get; set; }
    }

}
