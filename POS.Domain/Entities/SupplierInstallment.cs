using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class SupplierInstallment : Installment
    {

        public int PurchaseId { get; set; }

        public virtual Purchase Purchase { get; set; }

    }
}
