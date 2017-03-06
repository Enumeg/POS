using System.Collections.Generic;
using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using POS.Domain.Interfaces;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public class SuppliersService : ServicesBase, ISuppliersService
    {
        async Task<bool> ISuppliersService.AddSupplier(Supplier supplier)
        {
            return await CrudService.Add(supplier, c => c.Name == supplier.Name);
        }

        async Task<bool?> ISuppliersService.UpdateSupplier(Supplier supplier)
        {
            return await CrudService.Update(supplier, supplier.Id, c => c.Name == supplier.Name && c.Id != supplier.Id);
        }

        async Task<bool?> ISuppliersService.DeleteSupplier(int supplierId, bool removeRelatedEntities)
        {
            var supplier = Context.Suppliers.Include(u => u.Purchases).Include(u => u.PurchaseBacks).FirstOrDefault(c => c.Id == supplierId);
            if (supplier == null) return false;
            if (removeRelatedEntities)
            {
                Context.PurchasesDetails.RemoveRange(
                    Context.PurchasesDetails.Where(d => Context.Purchases.Any(p => p.Id == d.PurchaseId)));
                Context.Purchases.RemoveRange(supplier.Purchases);
                Context.PurchaseBackDetails.RemoveRange(
                    Context.PurchaseBackDetails.Where(d => Context.PurchasesDetails.Any(p => p.Id == d.PurchaseBackId)));
                Context.PurchasesBack.RemoveRange(supplier.PurchaseBacks);
                Context.Suppliers.Remove(supplier);
                await Context.SaveChangesAsync();
                return true;
            }
            else
            {
                if (supplier.Purchases.Count > 0) return null;
                if (supplier.PurchaseBacks.Count > 0) return null;

            }
            Context.Suppliers.Remove(supplier);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Supplier> ISuppliersService.FindSupplier(int supplierId)
        {
            return await Context.Suppliers.FindAsync(supplierId);
        }

        async Task<List<Supplier>> ISuppliersService.GetAllSuppliers()
        {
            return await Context.Suppliers.ToListAsync();
        }

    }

}
