using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class CustomersService : ServicesBase, ICustomersService
    {
        async Task<bool> ICustomersService.AddCustomer(Customer customer)
        {
            return await CrudService.Add(customer, c => c.Name == customer.Name);
        }

        async Task<bool?> ICustomersService.UpdateCustomer(Customer customer)
        {
            return await CrudService.Update(customer, customer.Id, c => c.Name == customer.Name && c.Id != customer.Id);
        }

        async Task<bool?> ICustomersService.DeleteCustomer(int customerId, bool removeRelatedEntities)
        {
            var customer = Context.Customers.Include(u => u.Sales).Include(u => u.SaleBacks).FirstOrDefault(c => c.Id == customerId);
            if (customer == null) return false;
            if (removeRelatedEntities)
            {
                Context.SaleDetails.RemoveRange(
                    Context.SaleDetails.Where(d => Context.Sales.Any(p => p.Id == d.SaleId)));
                Context.Sales.RemoveRange(customer.Sales);
                Context.SaleBackDetails.RemoveRange(
                    Context.SaleBackDetails.Where(d => Context.SaleDetails.Any(p => p.Id == d.SaleBackId)));
                Context.SaleBacks.RemoveRange(customer.SaleBacks);
                Context.Customers.Remove(customer);
                await Context.SaveChangesAsync();
                return true;
            }
            else
            {
                if (customer.Sales.Count > 0) return null;
                if (customer.SaleBacks.Count > 0) return null;

            }
            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<Customer> ICustomersService.FindCustomer(int customerId)
        {
            return await Context.Customers.FindAsync(customerId);
        }

        async Task<List<Customer>> ICustomersService.GetAllCustomers()
        {
            return await Context.Customers.ToListAsync();
        }
    }
}
