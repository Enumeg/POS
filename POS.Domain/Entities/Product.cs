using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Properties = new List<ProductProperty>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public  string BarCode { get; set; }
        public decimal SalePrice { get; set; }   
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public ICollection<ProductProperty> Properties { get; set; }


    }
}
