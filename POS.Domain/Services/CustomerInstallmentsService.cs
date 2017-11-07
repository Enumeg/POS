using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface ICustomerInstallmentsService : IInitializer
    {
        Task<bool> AddCustomerInstallment(CustomerInstallment installment);
        Task<bool?> UpdateCustomerInstallment(CustomerInstallment installment);
        Task<bool?> DeleteCustomerInstallment(int installmentId);
        Task<CustomerInstallment> FindCustomerInstallment(int installmentId);
        Task<List<CustomerInstallment>> GetAllCustomerInstallments();
    }
    public class CustomerInstallmentsService : ServicesBase, ICustomerInstallmentsService
    {
        async Task<bool> ICustomerInstallmentsService.AddCustomerInstallment(CustomerInstallment installment)
        {
            return await CrudService.Add(installment, c => c.SaleId == installment.SaleId && c.DueDate == installment.DueDate);
        }

        async Task<bool?> ICustomerInstallmentsService.UpdateCustomerInstallment(CustomerInstallment installment)
        {
            return await CrudService.Update(installment, installment.Id, c => c.SaleId == installment.SaleId && c.DueDate == installment.DueDate
                                                        && c.Id != installment.Id);
        }

        async Task<bool?> ICustomerInstallmentsService.DeleteCustomerInstallment(int installmentId)
        {
            var installment = Context.CustomerInstallments.Find(installmentId);
            if (installment == null) return false;        
            Context.CustomerInstallments.Remove(installment);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<CustomerInstallment> ICustomerInstallmentsService.FindCustomerInstallment(int installmentId)
        {
            return await Context.CustomerInstallments.FindAsync(installmentId);
        }       

        async Task<List<CustomerInstallment>> ICustomerInstallmentsService.GetAllCustomerInstallments()
        {
            return await Context.CustomerInstallments.ToListAsync();
        }

    }

}

