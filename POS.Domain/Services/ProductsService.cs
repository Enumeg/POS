﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class ProductsService : ServicesBase, IProductsService
    {
        public ProductsService(PosContext context) : base(context)
        {

        }
        async Task<bool> IProductsService.AddProduct(Product product)
        {
            return await CrudService.Add(product, c => (c.ArabicName == product.ArabicName || c.EnglishName == product.EnglishName) || (c.Barcode != "" && c.Barcode == product.Barcode));
        }
        async Task<bool> IProductsService.AddProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                await CrudService.Add(product, c => (c.ArabicName == product.ArabicName || c.EnglishName == product.EnglishName) || (c.Barcode != "" && c.Barcode == product.Barcode), false);
            }
            return await Context.SaveChangesAsync() > 0;
        }
        async Task<bool?> IProductsService.UpdateProduct(Product product)
        {
            var oldProduct = await Context.Products.FindAsync(product.Id);
            if (oldProduct == null)
                return null;
            if (await Context.Products.AnyAsync(c => (c.ArabicName == product.ArabicName || c.EnglishName == product.EnglishName) && c.Id != product.Id))
                return false;

            Context.ProductProperties.RemoveRange(Context.ProductProperties.Where(p => p.ProductId == product.Id));
            Context.ProductProperties.AddRange(product.Properties);
            oldProduct.UnitId = product.UnitId;
            oldProduct.Barcode = product.Barcode;
            oldProduct.SalePrice = product.SalePrice;
            oldProduct.ArabicName = product.ArabicName;
            oldProduct.EnglishName = product.EnglishName;
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<bool?> IProductsService.DeleteProduct(int productId, bool removeRelatedEntities)
        {
            var product = Context.Products.Include(p => p.TransfareDetails).Include(p => p.TransfareDetails).Include(p => p.Barcodes).Include(p => p.Properties).FirstOrDefault(c => c.Id == productId);
            if (product == null) return false;
            if (product.TransfareDetails.Count > 0)
                if (removeRelatedEntities)
                {
                    Context.TransfareDetails.RemoveRange(product.TransfareDetails);
                }
                else
                    return null;
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Product> IProductsService.FindProduct(int productId)
        {
            return await Context.Products.FindAsync(productId);
        }

        async Task<List<Product>> IProductsService.GetAllProducts()
        {
            //await Context.Properties.LoadAsync();
            return await Context.Products.Include("Category").Include("Unit").Include("Properties").ToListAsync();
        }

        async Task<Product> IProductsService.GetProduct(string barcode)
        {
            var product = await Context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
            return product ?? (await Context.BarCodes.Include(b => b.Product).FirstOrDefaultAsync(b => b.Barcode == barcode))?.Product;
        }
    }

}
