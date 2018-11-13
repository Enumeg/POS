using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class TransactionDetailBarcode
    {
        public int Id { get; set; }
        public int BarCodeId { get; set; }
        public int TransactionDetailId { get; set; }
        public virtual BarCode BarCode { get; set; }    
        public virtual TransactionDetail TransactionDetail { get; set; }

    }  

}
