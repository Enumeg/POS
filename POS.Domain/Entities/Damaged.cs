using System.Collections.Generic;

namespace POS.Domain.Entities
{

   public class Damaged
    {
        public Damaged()
        {
            DamagedDetails = new List<DamagedDetail>();
        }
        public int Id { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual ICollection<DamagedDetail> DamagedDetails { get; set; }
    }

}
