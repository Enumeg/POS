using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; }

    }
}
