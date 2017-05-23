﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Purchase : Transaction
    {
        public Purchase()
        {
            PurchaseDetails = new List<PurchaseDetail>();
            PurchaseBacks = new List<PurchaseBack>();
            Installments = new List<SupplierInstallment>();
            Cheques = new List<SupplierCheque>();
        }   
        public PaymentMethod PaymentMethod { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int PointId { get; set; }
        public virtual Point Point { get; set; }
        public virtual WithDrawal WithDrawal { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public virtual ICollection<PurchaseBack> PurchaseBacks { get; set; }
        public virtual ICollection<SupplierInstallment> Installments { get; set; }
        public virtual ICollection<SupplierCheque> Cheques { get; set; }
        [NotMapped]
        public decimal Total => PurchaseDetails.Sum(s => s.Total);




    }

}
