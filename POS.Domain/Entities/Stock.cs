using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Stock
    { 
        [Key,Column(Order =1)]
        public int ProductId { get; set; }
        [Key, Column(Order = 2)]
        public int PointId { get; set; }
        public int TenantId { get; set; }
        public decimal Amount { get; set; }
        public virtual Product Product { get; set; }
        public virtual Point Point { get; set; }
    }
}
