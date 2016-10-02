﻿using System.Collections.Generic;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{

   public class Transfare 
    {
        public Transfare()
        {
            TransfareDetails = new List<TransfareDetail>();
        }      
        public int FromPointId { get; set; }
        public virtual Point FromPoint { get; set; }
        public int ToPointId { get; set; }
        public virtual Point ToPoint { get; set; }
        public virtual ICollection<TransfareDetail> TransfareDetails { get; set; }
    }

}