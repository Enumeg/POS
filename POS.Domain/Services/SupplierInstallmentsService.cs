using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Domain.Services
{
    public interface ISupplierInstallmentsService : IInitializer
    {
        Task<bool> AddSupplierInstallment(SupplierInstallment installment);
        Task<bool?> UpdateSupplierInstallment(SupplierInstallment installment);
        Task<bool?> DeleteSupplierInstallment(int installmentId);
        Task<SupplierInstallment> FindSupplierInstallment(int installmentId);
        Task<List<SupplierInstallment>> GetAllSupplierInstallments();
    }
    public class SupplierInstallmentsService : ServicesBase, ISupplierInstallmentsService
    {
        async Task<bool> ISupplierInstallmentsService.AddSupplierInstallment(SupplierInstallment installment)
        {
            return await CrudService.Add(installment, c => c.PurchaseId == installment.PurchaseId && c.DueDate == installment.DueDate);
        }

        async Task<bool?> ISupplierInstallmentsService.UpdateSupplierInstallment(SupplierInstallment installment)
        {
            return await CrudService.Update(installment, installment.Id, c => c.PurchaseId == installment.PurchaseId && c.DueDate == installment.DueDate
                                                        && c.Id != installment.Id);
        }

        async Task<bool?> ISupplierInstallmentsService.DeleteSupplierInstallment(int installmentId)
        {
            var installment = Context.SupplierInstallments.Find(installmentId);
            if (installment == null) return false;        
            Context.SupplierInstallments.Remove(installment);
            await Context.SaveChangesAsync();
            return true;
        }

        async Task<SupplierInstallment> ISupplierInstallmentsService.FindSupplierInstallment(int installmentId)
        {
            return await Context.SupplierInstallments.FindAsync(installmentId);
        }       

        async Task<List<SupplierInstallment>> ISupplierInstallmentsService.GetAllSupplierInstallments()
        {
            return await Context.SupplierInstallments.ToListAsync();
        }

    }

}

