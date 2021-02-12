using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Enums;

namespace POS.Domain.Models
{
    public class StockModel
    {
        public StockModel(Stock stock)
        {
            Product = new ProductModel(stock.Product);
            Point = stock.Point?.Name;
            PointType = stock.Point?.PointType;
            Amount = stock.Amount;
        }
        public ProductModel  Product { get; set; }
        public string Point { get; set; }
        public PointType? PointType { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProductModel
    {
        public ProductModel(Product product)
        {
            Name = product.Name;
            Barcode = product.Barcode;
            SalePrice = product.SalePrice;
            Category = product.Category?.Name;
            Unit = product.Unit?.Name;
        }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal SalePrice { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
    }

}
