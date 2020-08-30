using System.Collections.Generic;

namespace POS.Domain.Entities
{

   public class Damaged : EntityBase
    {
        public Damaged()
        {
            DamagedDetails = new List<DamagedDetail>();
        }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<DamagedDetail> DamagedDetails { get; set; }
    }

}
