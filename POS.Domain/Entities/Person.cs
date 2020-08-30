﻿using System.Collections.Generic;
using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public PersonType PersonType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
