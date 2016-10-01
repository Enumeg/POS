using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class PurchaseBackDetail : TransactionDetail
    {
        public int PurchaseBackId { get; set; }
        public virtual PurchaseBack PurchaseBack { get; set; }
    }
}
