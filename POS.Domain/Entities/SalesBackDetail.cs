using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class SalesBackDetail : TransactionDetail
    {
        public int SalesBackId { get; set; }
        public virtual SalesBack SalesBack { get; set; }
    }
}
