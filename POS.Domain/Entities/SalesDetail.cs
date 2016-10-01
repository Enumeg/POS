using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class SalesDetail : TransactionDetail
    {
        public int SalesId { get; set; }
        public virtual Sales Sales { get; set; }
    }
}
