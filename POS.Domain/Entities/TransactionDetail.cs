﻿using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Entities
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public decimal Total => Amount * Price;
    }
}
