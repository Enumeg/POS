﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
   public class ProductDetail : EntityBase
    {
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
