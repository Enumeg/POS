using System.Collections.Generic;

namespace POS.Domain.Entities
{
    public class Machine : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }= new List<Shift>();
        
    }
}