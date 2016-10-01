using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class SaleBackDetail : TransactionDetail
    {
        public int SaleBackId { get; set; }
        public virtual SaleBack SaleBack { get; set; }
    }
}
