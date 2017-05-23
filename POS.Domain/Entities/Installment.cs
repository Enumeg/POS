﻿using System;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? ShiftId { get; set; }

        public virtual Shift Shift { get; set; }

    }
    public class CustomerInstallment : Installment
    {
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
    public class SupplierInstallment : Installment
    {
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
