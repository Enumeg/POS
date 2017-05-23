using POS.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Bank
    {
        public Bank()
        {
            BankAccounts = new HashSet<BankAccount>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

    }
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }    
        public virtual ICollection<SupplierCheque> SupplierCheques { get; set; }    
        public virtual ICollection<CustomerCheque> CustomerCheques { get; set; }    
        public virtual ICollection<Deposit> Deposits { get; set; }    
        public virtual ICollection<WithDrawal> WithDrawals { get; set; }    

        [NotMapped]
        public string Label => Name + " - " + Number; 
    }

}
