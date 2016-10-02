using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class CustomerInstallment : Installment
    {

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

    }
}
