using System.Collections.Generic;
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
        async Task<bool> IProductsService.AddProduct(Product product)
        {
            return await CrudService.Add(product, c => c.Name == product.Name);
        }

        async Task<bool?> IProductsService.UpdateProduct(Product product)
        {
            return await CrudService.Update(product, product.Id, c => c.Name == product.Name && c.Id != product.Id);
        }

        async Task<bool?> IProductsService.DeleteProduct(int productId, bool removeRelatedEntities)
        {
            var product = Context.Products.Include(p => p.TransactionDetails).Include(p=> p.TransfareDetails).Include(p=>p.Barcodes).Include(p=>p.Properties).FirstOrDefault(c => c.Id == productId);
            if (product == null) return false;
            if (product.TransactionDetails.Count > 0)
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
            await Context.Properties.LoadAsync();
            return await Context.Products.Include("Category").Include("Unit").Include("Properties").ToListAsync();
        }

    }

}
